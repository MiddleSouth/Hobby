<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\MusicScoreProduct;
use App\MusicScoreProductNumbering;
use Illuminate\Support\Facades\DB;
use App\Http\Requests\MusicScoreProductNumberingRequest;

class MusicScoreProductNumberingsController extends Controller
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
        $products = MusicScoreProduct::numberingsIndex()->get();
        $dropdownProducts = MusicScoreProduct::orderBy('order', 'asc')->pluck('name', 'id');

        return view('scoreNumberings.index', compact('products', 'dropdownProducts'));
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  \App\Http\Requests\MusicScoreProductNumberingRequest  $request
     * @return \Illuminate\Http\Response
     */
    public function store(MusicScoreProductNumberingRequest $request)
    {
        $validated = $request->validated();

        // 掲載順の最大値チェック
        $maxOrder = MusicScoreProductNumbering::maxOrder($validated['product_id'])->first()->order + 1;
        if($validated['order'] > $maxOrder)
        {
            $validated['order'] = $maxOrder;
        }

        DB::transaction(function() use($validated){
            MusicScoreProductNumbering::sortInsert($validated['product_id'], $validated['order']);
            MusicScoreProductNumbering::create($validated);
        });

        return redirect()->route('scoreNumberings.index')
            ->with('message', '楽譜のナンバリングマスタを登録しました');
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \App\Http\Requests\MusicScoreProductNumberingRequest  $request
     * @param  MusicScoreProductNumbering  $scoreNumbering
     * @return \Illuminate\Http\Response
     */
    public function update(MusicScoreProductNumberingRequest $request, MusicScoreProductNumbering $scoreNumbering)
    {
        $validated = $request->validated();

        DB::transaction(function() use($validated, $scoreNumbering){
            // 掲載順の最大値
            $maxOrder = MusicScoreProductNumbering::maxOrder($validated['product_id'])->first()->order;

            // 別のカテゴリに変更する場合
            if($scoreNumbering->product_id != $validated['product_id'])
            {
                // 掲載順の最大値調整
                $maxOrder += 1;

                // 変更前カテゴリーの掲載順を調整
                MusicScoreProductNumbering::sortDelete($scoreNumbering->product_id, $scoreNumbering->order);

                // 変更後カテゴリーの掲載順を調整
                MusicScoreProductNumbering::sortInsert($validated['product_id'], $validated['order']);
            }
            else
            {
                // 同じカテゴリ内で掲載順に変更がある場合
                if($scoreNumbering->order !== $validated['order'])
                {
                    // 同じカテゴリーの掲載順を調整
                    MusicScoreProductNumbering::sortUpdate($scoreNumbering->product_id, $scoreNumbering->order, $validated['order']);
                }
            }

            // 掲載順の最大値チェック
            if($validated['order'] > $maxOrder)
            {
                $validated['order'] = $maxOrder;
            }

            $scoreNumbering->update($validated);
        });

        return redirect()->route('scoreNumberings.index')
            ->with('message', '楽譜のナンバリングマスタを更新しました');
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  MusicScoreProductNumbering  $scoreNumbering
     * @return \Illuminate\Http\Response
     */
    public function destroy(MusicScoreProductNumbering $scoreNumbering)
    {
        DB::transaction(function() use($scoreNumbering){
            MusicScoreProductNumbering::sortDelete($scoreNumbering->product_id, $scoreNumbering->order);
            $scoreNumbering->delete();
        });

        return redirect()->route('scoreNumberings.index')
            ->with('message', '楽譜のナンバリングマスタを削除しました');
    }
}
