<?php

namespace App;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Support\Facades\DB;

class MusicScoreProduct extends Model
{
    protected $fillable = ['name','order'];

    protected $casts = [
        'id' => 'int'
    ];

    /**
     * リレーション設定 Products -> Numberibngs
     */
    public function musicScoreProductNumberings()
    {
        return $this->hasMany('App\MusicScoreProductNumbering', 'product_id')->orderBy('order', 'asc');
    }

    /**
     * すべての作品、ナンバリング、楽譜を取得
     * 
     * @param \Illuminate\Database\Eloquent\Builder $query
     * @return \Illuminate\Database\Eloquent\Builder
     */
    public function scopeAllScores($query)
    {
        return $query->orderBy('order', 'asc')->with('musicScoreProductNumberings.musicScores');
    }

    /**
     * 作品管理ページのデータ取得
     * 
     * @param \Illuminate\Database\Eloquent\Builder $query
     * @return \Illuminate\Database\Eloquent\Builder
     */
    public function scopeNumberingsIndex($query)
    {
        // Numberingsのソートはリレーションにて設定済
        return $query->orderBy('order', 'asc')
            ->with(['musicScoreProductNumberings' => function($query){
                $query->selectWithScoreCount();
            }]);
    }

    /**
     * 各ナンバリングタイトルに紐づく楽譜件数のカラムを含んだSelect文を生成する
     * 
     * @param \Illuminate\Database\Eloquent\Builder $query
     * @return \Illuminate\Database\Eloquent\Builder
     */
    public function scopeSelectWithNumberingCount($query)
    {
        return $query->select(DB::raw('
                music_score_products.id as id
                ,music_score_products.name as name
                ,music_score_products.[order] as [order]
                ,(
                    select count(*) from music_score_Product_Numberings
                    where music_score_Product_Numberings.product_id = music_score_products.id
                ) as product_count
                '))
                ->orderBy('order', 'asc');
    }

    /**
     * 掲載順の最大値取得
     * 
     * @param \Illuminate\Database\Eloquent\Builder $query
     * @return \Illuminate\Database\Eloquent\Builder
     */
    public function scopeMaxOrder($query)
    {
        return $query->orderBy('order', 'desc');
    }

    /**
     * データの挿入によるソート番号の変更を行う
     * 
     * @param \Illuminate\Database\Eloquent\Builder $query
     * @param int $insertSort
     * @return \Illuminate\Database\Eloquent\Builder
     */
    public function scopeSortInsert($query, int $insertSort)
    {
        return $query->where('order', '>=', $insertSort)->increment('order');
    }

    /**
     * データの更新によるソート番号の変更を行う
     * 
     * @param \Illuminate\Database\Eloquent\Builder $query
     * @param int $beforeSort
     * @param int $afterSort
     * @return \Illuminate\Database\Eloquent\Builder
     */
    public function scopeSortUpdate($query, int $beforeSort, int $afterSort)
    {
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
     * @param int $deleteSort
     * @return \Illuminate\Database\Eloquent\Builder
     */
    public function scopeSortDelete($query, int $deleteSort)
    {
        return $query->where('order', '>', $deleteSort)->decrement('order');
    }


}
