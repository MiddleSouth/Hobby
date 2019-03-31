<?php

namespace App;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Support\Facades\DB;
use App\IllustCategory;

class IllustProduct extends Model
{
    protected $fillable = ['name', 'category_id', 'order'];

    protected $casts = [
        'id' => 'int'
        ,'category_id' => 'int'
        ,'order' => 'int'
    ];
    
    /**
     * リレーション設定 Categories -> Products
     */
    public function illustCategories()
    {
        return $this->belongsTo('App\IllustCategory', 'category_id');
    }

    /**
     * リレーション設定 Products -> Illusts
     */
    public function illusts()
    {
        return $this->hasMany('App\Illust', 'product_id')->latest('published_at');
    }

    /**
     * 作品データ取得時の基本のソート
     * 
     * @param \Illuminate\Database\Eloquent\Builder $query
     * @return \Illuminate\Database\Eloquent\Builder
     */
    public function scopeBasicOrder($query)
    {
        return $query->orderBy('category_id', 'asc')->orderBy('order', 'asc');
    }

    /**
     * 各作品に紐づくイラストの件数のカラムを含んだSelect文を生成する
     * 
     * @param \Illuminate\Database\Eloquent\Builder $query
     * @return \Illuminate\Database\Eloquent\Builder
     */
    public function scopeSelectWithIllustCount($query)
    {
        return $query->select(DB::raw('
                illust_products.id as id
                ,illust_products.name as name
                ,illust_products.category_id as category_id
                ,illust_products.[order] as [order]
                ,(
                    select count(*) from illusts
                    where illusts.product_id = illust_products.id
                ) as illust_count
                '));
    }

    /**
     * 掲載順の最大値取得
     * 
     * @param \Illuminate\Database\Eloquent\Builder $query
     * @param int $category_id
     * @return \Illuminate\Database\Eloquent\Builder
     */
    public function scopeMaxOrder($query, int $category_id)
    {
        return $query->where('category_id', $category_id)->orderBy('order', 'desc');
    }

    /**
     * データの挿入によるソート番号の変更を行う
     * 
     * @param \Illuminate\Database\Eloquent\Builder $query
     * @param int $category_id
     * @param int $insertSort
     * @return \Illuminate\Database\Eloquent\Builder
     */
    public function scopeSortInsert($query, int $category_id, int $insertSort)
    {
        return $query->where('category_id', $category_id)->where('order', '>=', $insertSort)->increment('order');
    }

    /**
     * データの更新によるソート番号の変更を行う
     * 
     * @param \Illuminate\Database\Eloquent\Builder $query
     * @param int $category_id
     * @param int $beforeSort
     * @param int $afterSort
     * @return \Illuminate\Database\Eloquent\Builder
     */
    public function scopeSortUpdate($query, int $category_id, int $beforeSort, int $afterSort)
    {
        $query->where('category_id', $category_id);
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
     * @param int $category_id
     * @param int $deleteSort
     * @return \Illuminate\Database\Eloquent\Builder
     */
    public function scopeSortDelete($query, int $category_id, int $deleteSort)
    {
        return $query->where('category_id', $category_id)->where('order', '>', $deleteSort)->decrement('order');
    }

}
