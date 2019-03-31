<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\IllustProduct;
use App\IllustCategory;
use Illuminate\Support\Facades\DB;
use App\Http\Requests\IllustProductRequest;

class IllustProductsController extends Controller
{
    public function __construct()
    {
        $this->middleware('auth');
    }

    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index()
    {
        $categories = IllustCategory::productsIndex()->get();
        $dropdownCategories = IllustCategory::orderBy('order', 'asc')->pluck('name', 'id');

        return view('illustProducts.index', compact('categories', 'dropdownCategories'));
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  \Illuminate\Http\Request\IllustProductRequest  $request
     * @return \Illuminate\Http\Response
     */
    public function store(IllustProductRequest $request)
    {
        $validated = $request->validated();

        // 掲載順の最大値チェック
        $maxOrder = IllustProduct::maxOrder($validated['category_id'])->first()->order + 1;
        if($validated['order'] > $maxOrder)
        {
            $validated['order'] = $maxOrder;
        }

        DB::transaction(function() use($validated){
            IllustProduct::sortInsert($validated['category_id'], $validated['order']);
            IllustProduct::create($validated);
        });

        return redirect()->route('illustProducts.index')
            ->with('message', 'イラストの作品マスタを登録しました');
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \Illuminate\Http\Request\IllustProductRequest  $request
     * @param  IllustProduct  $illustProduct
     * @return \Illuminate\Http\Response
     */
    public function update(IllustProductRequest $request, IllustProduct $illustProduct)
    {
        $validated = $request->validated();

        DB::transaction(function() use($validated, $illustProduct){
            // 掲載順の最大値
            $maxOrder = IllustProduct::maxOrder($validated['category_id'])->first()->order;

            // 別のカテゴリに変更する場合
            if($illustProduct->category_id != $validated['category_id'])
            {
                // 掲載順の最大値調整
                $maxOrder += 1;

                // 変更前カテゴリーの掲載順を調整
                IllustProduct::sortDelete($illustProduct->category_id, $illustProduct->order);

                // 変更後カテゴリーの掲載順を調整
                IllustProduct::sortInsert($validated['category_id'], $validated['order']);
            }
            else
            {
                // 同じカテゴリ内で掲載順に変更がある場合
                if($illustProduct->order !== $validated['order'])
                {
                    // 同じカテゴリーの掲載順を調整
                    IllustProduct::sortUpdate($illustProduct->category_id, $illustProduct->order, $validated['order']);
                }
            }

            // 掲載順の最大値チェック
            if($validated['order'] > $maxOrder)
            {
                $validated['order'] = $maxOrder;
            }

            $illustProduct->update($validated);
        });

        return redirect()->route('illustProducts.index')
            ->with('message', 'イラストの作品マスタを更新しました');
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  IllustProduct  $illustProduct
     * @return \Illuminate\Http\Response
     */
    public function destroy(IllustProduct $illustProduct)
    {
        DB::transaction(function() use($illustProduct){
            IllustProduct::sortDelete($illustProduct->category_id, $illustProduct->order);
            $illustProduct->delete();
        });

        return redirect()->route('illustProducts.index')
            ->with('message', 'イラストの作品を削除しました');
    }
}
