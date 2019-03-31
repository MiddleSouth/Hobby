<?php

namespace App;

use Illuminate\Database\Eloquent\Model;
use App\IllustProduct;

class IllustCategory extends Model
{
    protected $fillable = ['name'];

    protected $casts = [
        'id' => 'int'
        ,'order' => 'int'
    ];

    /**
     * リレーション設定 Categories -> Products
     */
    public function illustProducts()
    {
        return $this->hasMany('App\IllustProduct', 'category_id')->orderBy('order', 'asc');
    }

    /**
     * 検索された作品IDに紐づくカテゴリー、作品、イラストを検索
     * 
     * @param \Illuminate\Database\Eloquent\Builder $query
     * @param array $searchProductIds 検索対象の作品ID。空の配列を渡すと全件取得。
     * @return \Illuminate\Database\Eloquent\Builder
     */
    public function scopeSearchedCategories($query, array $searchProductIds)
    {
        // TODO:イラストが存在しない作品は検索対象外にする？
        if(count($searchProductIds) > 0 )
        {
            $query->with(['illustProducts' => function($query) use($searchProductIds){
                $query->whereIn('id', $searchProductIds);
            }])
            ->whereHas('illustProducts', function($query) use($searchProductIds){
                $query->whereIn('id', $searchProductIds)->with('illusts');
            });
        }
        else
        {
            $query->with('illustProducts.illusts');
        }

        return $query->orderBy('order', 'asc');
    }

    /**
     * 検索条件となるカテゴリー、作品一覧
     * 
     * @param \Illuminate\Database\Eloquent\Builder $query
     * @return \Illuminate\Database\Eloquent\Builder
     */
    public function scopeSearchConditions($query)
    {
        // IllustProductsのソートはリレーションにて設定済
        return $query->orderBy('order', 'asc')->with('illustProducts');
    }

    /**
     * 作品管理ページのデータ取得
     * 
     * @param \Illuminate\Database\Eloquent\Builder $query
     * @return \Illuminate\Database\Eloquent\Builder
     */
    public function scopeProductsIndex($query)
    {
        // IllustProductsのソートはリレーションにて設定済
        return $query->orderBy('order', 'asc')
            ->with(['illustProducts' => function($query){
                $query->selectWithIllustCount();
            }]);
    }


}
