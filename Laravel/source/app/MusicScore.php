<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class MusicScore extends Model
{
    protected $fillable = ['numbering_id', 'nico_movie_id', 'title', 'description', 'order', 'published_at'];

    protected $dates = ['published_at'];

    protected $casts = [
        'id' => 'int'
        ,'numbering_id' => 'int'
        ,'order' => 'int'
    ];
    
    /**
     * リレーション設定 Products -> Numberibngs
     */
    public function musicScoreProductNumberings()
    {
        return $this->belongsTo('App\MusicScoreProductNumbering', 'numbering_id');
    }

    /**
     * 掲載順の最大値取得
     * 
     * @param \Illuminate\Database\Eloquent\Builder $query
     * @param int $numbering_id
     * @return \Illuminate\Database\Eloquent\Builder
     */
    public function scopeMaxOrder($query, int $numbering_id)
    {
        return $query->where('numbering_id', $numbering_id)->orderBy('order', 'desc');
    }

    /**
     * データの挿入によるソート番号の変更を行う
     * 
     * @param \Illuminate\Database\Eloquent\Builder $query
     * @param int $numbering_id
     * @param int $insertSort
     * @return \Illuminate\Database\Eloquent\Builder
     */
    public function scopeSortInsert($query, int $numbering_id, int $insertSort)
    {
        return $query->where('numbering_id', $numbering_id)->where('order', '>=', $insertSort)->increment('order');
    }

    /**
     * データの更新によるソート番号の変更を行う
     * 
     * @param \Illuminate\Database\Eloquent\Builder $query
     * @param int $numbering_id
     * @param int $beforeSort
     * @param int $afterSort
     * @return \Illuminate\Database\Eloquent\Builder
     */
    public function scopeSortUpdate($query, int $numbering_id, int $beforeSort, int $afterSort)
    {
        $query->where('numbering_id', $numbering_id);
        if($beforeSort < $afterSort)
        {
            return $query->whereBetween('order', [$beforeSort+1, $afterSort])->decrement('order');
        }
        else
        {
            return $query->whereBetween('order', [$afterSort, $beforeSort-1])->increment('order');
        }
    }

    /**
     * データの削除によるソート番号の変更を行う
     * 
     * @param \Illuminate\Database\Eloquent\Builder $query
     * @param int $numbering_id
     * @param int $deleteSort
     * @return \Illuminate\Database\Eloquent\Builder
     */
    public function scopeSortDelete($query, int $numbering_id, int $deleteSort)
    {
        return $query->where('numbering_id', $numbering_id)->where('order', '>', $deleteSort)->decrement('order');
    }

    /**
     * 最新の楽譜データを取得する
     * 
     * @param \Illuminate\Database\Eloquent\Builder $query
     * @return stdClass
     */
    public function scopeGetLatestScore($query)
    {
        return $query->orderBy('published_at', 'desc')->first();
    }

    /**
     * 最新のIDを取得する
     * 
     * @param \Illuminate\Database\Eloquent\Builder $query
     * @return int $id
     */
    public function scopeGetLatestId($query)
    {
        return $query->orderBy('id', 'desc')->first()->id;
    }

}
