<?php

namespace App;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Support\Facades\DB;

class MusicScoreProductNumbering extends Model
{
    protected $fillable = ['product_id','name','short_name','order'];

    protected $casts = [
        'id' => 'int'
        ,'product_id' => 'int'
        ,'order' => 'int'
    ];
    
    /**
     * リレーション設定 Products -> Numberibngs
     */
    public function musicScoreProducts()
    {
        return $this->belongsTo('App\MusicScoreProduct', 'product_id');
    }

    /**
     * リレーション設定 Numberibngs -> MusicScores
     */
    public function musicScores()
    {
        return $this->hasMany('App\MusicScore', 'numbering_id')->orderBy('order');
    }

    /**
     * 作品-ナンバリングのソートを行う
     * 
     * @param \Illuminate\Database\Eloquent\Builder $query
     * @return \Illuminate\Database\Eloquent\Builder
     */
    public function scopeBasicSort($query)
    {
        return $query->orderBy('product_id', 'asc')->orderBy('sort', 'asc');
    }

    /**
     * 各ナンバリングタイトルに紐づく楽譜件数のカラムを含んだSelect文を生成する
     * 
     * @param \Illuminate\Database\Eloquent\Builder $query
     * @return \Illuminate\Database\Eloquent\Builder
     */
    public function scopeSelectWithScoreCount($query)
    {
        return $query->select(DB::raw('
                music_score_product_numberings.id as id
                ,music_score_product_numberings.product_id as product_id
                ,music_score_product_numberings.name as name
                ,music_score_product_numberings.short_name as short_name
                ,music_score_product_numberings.[order] as [order]
                ,(
                    select count(*) from music_scores
                    where music_scores.numbering_id = music_score_product_numberings.id
                ) as score_count
                '));
    }

    /**
     * 掲載順の最大値取得
     * 
     * @param \Illuminate\Database\Eloquent\Builder $query
     * @param int $product_id
     * @return \Illuminate\Database\Eloquent\Builder
     */
    public function scopeMaxOrder($query, int $product_id)
    {
        return $query->where('product_id', $product_id)->orderBy('order', 'desc');
    }

    /**
     * データの挿入によるソート番号の変更を行う
     * 
     * @param \Illuminate\Database\Eloquent\Builder $query
     * @param int $product_id
     * @param int $insertSort
     * @return \Illuminate\Database\Eloquent\Builder
     */
    public function scopeSortInsert($query, int $product_id, int $insertSort)
    {
        return $query->where('product_id', $product_id)->where('order', '>=', $insertSort)->increment('order');
    }

    /**
     * データの更新によるソート番号の変更を行う
     * 
     * @param \Illuminate\Database\Eloquent\Builder $query
     * @param int $product_id
     * @param int $beforeSort
     * @param int $afterSort
     * @return \Illuminate\Database\Eloquent\Builder
     */
    public function scopeSortUpdate($query, int $product_id, int $beforeSort, int $afterSort)
    {
        $query->where('product_id', $product_id);
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
     * @param int $product_id
     * @param int $deleteSort
     * @return \Illuminate\Database\Eloquent\Builder
     */
    public function scopeSortDelete($query, int $product_id, int $deleteSort)
    {
        return $query->where('product_id', $product_id)->where('order', '>', $deleteSort)->decrement('order');
    }

}
