<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\MusicScoreProduct;
use Illuminate\Support\Facades\DB;
use App\Http\Requests\MusicScoreProductRequest;

class MusicScoreProductsController extends Controller
{
    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index()
    {
        $products = MusicScoreProduct::selectWithNumberingCount()->get();

        return view('scoreProducts.index', compact('products'));
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  \App\Http\Requests\MusicScoreProductRequest  $request
     * @return \Illuminate\Http\Response
     */
    public function store(MusicScoreProductRequest $request)
    {
        $validated = $request->validated();

        // 掲載順の最大値チェック
        $maxOrder = MusicScoreProduct::maxOrder()->first()->order + 1;
        if($validated['order'] > $maxOrder)
        {
            $validated['order'] = $maxOrder;
        }

        DB::transaction(function() use($validated){
            MusicScoreProduct::sortInsert($validated['order']);
            MusicScoreProduct::create($validated);
        });

        return redirect()->route('scoreProducts.index')
            ->with('message', '楽譜の作品マスタを登録しました');
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \App\Http\Requests\MusicScoreProductRequest  $request
     * @param  MusicScoreProduct  $scoreProduct
     * @return \Illuminate\Http\Response
     */
    public function update(MusicScoreProductRequest $request, MusicScoreProduct $scoreProduct)
    {
        $validated = $request->validated();

        DB::transaction(function() use($validated, $scoreProduct){

            // 掲載順の最大値チェック
            $maxOrder = MusicScoreProduct::maxOrder()->first()->order;
            if($validated['order'] > $maxOrder)
            {
                $validated['order'] = $maxOrder;
            }

            // 同じカテゴリ内で掲載順に変更がある場合
            if($scoreProduct->order !== $validated['order'])
            {
                // 同じカテゴリーの掲載順を調整
                MusicScoreProduct::sortUpdate($scoreProduct->order, $validated['order']);
            }

            $scoreProduct->update($validated);
        });

        return redirect()->route('scoreProducts.index')
            ->with('message', '楽譜の作品マスタを更新しました');
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  MusicScoreProduct  $scoreProduct
     * @return \Illuminate\Http\Response
     */
    public function destroy(MusicScoreProduct $scoreProduct)
    {
        DB::transaction(function() use($scoreProduct){
            MusicScoreProduct::sortDelete($scoreProduct->order);
            $scoreProduct->delete();
        });

        return redirect()->route('scoreProducts.index')
            ->with('message', '楽譜の作品マスタを削除しました');
    }
}
