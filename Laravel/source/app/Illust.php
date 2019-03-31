<?php

namespace App;

use Illuminate\Database\Eloquent\Model;
use Carbon\Carbon;
use App\IllustProduct;

class Illust extends Model
{
    protected $fillable = ['back_ground_id', 'product_id', 'title', 'caption', 'illust_file_name', 'thumbnail_file_name', 'published_at'];

    protected $dates = ['published_at'];

    protected $casts = [
        'id' => 'int'
        ,'back_ground_id' => 'int'
        ,'product_id' => 'int'
    ];
    
    /**
     * リレーション設定 Products -> Illusts
     */
    public function illustProducts()
    {
        return $this->belongsTo('App\IllustProduct', 'product_id');
    }

    /**
     * 新着イラスト
     * 
     * @param \Illuminate\Database\Eloquent\Builder $query
     * @param int $limit 取得数の上限
     * @return \Illuminate\Database\Eloquent\Builder
     */
    public function scopeNewArrivals($query, int $limit)
    {
        return $query->latest('published_at')->limit($limit);
    }

    /**
     * 次のイラストを取得する
     * 
     * @param int $currentIllustId 現在のイラストのID
     * @return \App\Illust
     */
    public function getNextIllust(int $currentIllustId)
    {
        return $this->getPrevOrNextIllust($currentIllustId, true);
    }

    /**
     * 前のイラストを取得する
     * 
     * @param int $currentIllustId 現在のイラストのID
     * @return \App\Illust
     */
    public function getPrevIllust(int $currentIllustId)
    {
        return $this->getPrevOrNextIllust($currentIllustId, false);
    }

    /**
     * 前、または次のイラストを取得する
     * 
     * @param \Illuminate\Database\Eloquent\Builder $query
     * @param bool $isNext
     * @return \Illuminate\Database\Eloquent\Builder
     */
    public function scopePrevOrNextIllust($query, int $currentIllustId, bool $isNext)
    {
        $orderSort;
        $publishedSort;
        if($isNext === true)
        {
            $orderSort = 'desc';
            $publishedSort = 'asc';
        }
        else
        {
            $orderSort = 'asc';
            $publishedSort = 'desc';
        }

        $illusts =
            Illust::join('illust_products', 'illust_products.id', '=', 'illusts.product_id')
                ->join('illust_categories', 'illust_categories.id', '=', 'illust_products.category_id')
                ->select(
                    'illusts.id as id'
                )
                ->orderBy('illust_categories.order', $orderSort)
                ->orderBy('illust_products.order', $orderSort)
                ->orderBy('illusts.published_at', $publishedSort)
                ->get();

        $foundCurrentId = false;
        $returnId = -1;

        foreach($illusts as $illust)
        {
            if($foundCurrentId === true)
            {
                $returnId = $illust->id;
                break;
            }

            if($illust->id === $currentIllustId)
            {
                $foundCurrentId = true;
            }
        }

        return $query->where('id', $returnId);
    }

}
