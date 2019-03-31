<?php

namespace App\Http\Controllers;

use Illuminate\Support\Facades\DB;
use Illuminate\Support\Facades\Storage;
use Illuminate\Http\Request;
use App\MusicScore;
use App\MusicScoreProduct;
use App\MusicScoreProductNumbering;
use App\Http\Requests\MusicScoreRequest;

class MusicScoreController extends Controller
{
    public function __construct()
    {
        $this->middleware('auth')
            ->except(['index','show']);
    }

    /**
     * Display a listing of the resource.
     *
     * @param \App\MusicScore $showScore
     * @return \Illuminate\Http\Response
     */
    public function index($showScore = null)
    {
        if($showScore === null)
        {
            $showScore = MusicScore::getLatestScore();
        }
        $showNumbering = MusicScoreProductNumbering::find($showScore->numbering_id);

        $products = MusicScoreProduct::allScores()->get();

        return view('scores.index', compact('products','showScore','showNumbering'));
    }

    /**
     * Show the form for creating a new resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function create()
    {
        $products = MusicScoreProduct::allScores()->get();
        $numberings = MusicScoreProductNumbering::basicSort()->pluck('name', 'id');

        return view('scores.create', compact('products', 'numberings'));
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  \App\Http\Requests\MusicScoreRequest  $request
     * @return \Illuminate\Http\Response
     */
    public function store(MusicScoreRequest $request)
    {
        // 楽譜・MP3ファイルのアップロード確認
        if ($request->file('score_file')->isValid()
            && $request->file('music_file')->isValid())
        {
            DB::transaction(function() use($request){
                // 掲載順の最大値
                $maxOrder = MusicScore::maxOrder($request['numbering_id'])->first()->order + 1;

                MusicScore::sortInsert($request['numbering_id'], $request['order']);

                // 掲載順の最大値チェック
                if($request['order'] > $maxOrder)
                {
                    $request['order'] = $maxOrder;
                }

                MusicScore::create($request
                    ->only(['numbering_id','nico_movie_id','title','description','order','published_at'])
                );

                $id = MusicScore::getLatestId();
                $request->score_file->storeAs('public/' . config('const.SCORE_DIRECTORY_NAME'), $id . '.pdf');
                $request->music_file->storeAs('public/' . config('const.MUSIC_DIRECTORY_NAME'), $id . '.mp3');
            });
        }
        else
        {
            return redirect()->back()->withInput()->withErrors(['file' => '楽譜またはMP3ファイルがアップロードされていない、または不正なデータです。']);
        }

        return redirect()->route('scores.index')->with('message', '楽譜を追加しました');
    }

    /**
     * Display the specified resource.
     *
     * @param  \App\MusicScore $score
     * @return \Illuminate\Http\Response
     */
    public function show(MusicScore $score)
    {
        return $this->index($score);
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  \App\MusicScore  $score
     * @return \Illuminate\Http\Response
     */
    public function edit(MusicScore $score)
    {
        $products = MusicScoreProduct::allScores()->get();
        $numberings = MusicScoreProductNumbering::basicSort()->pluck('name', 'id');

        return view('scores.edit', compact('score', 'products', 'numberings'));
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \App\Http\Requests\MusicScoreRequest  $request
     * @param  \App\MusicScore  $score
     * @return \Illuminate\Http\Response
     */
    public function update(MusicScoreRequest $request, MusicScore $score)
    {
        $validated = $request->validated();

        DB::transaction(function() use($validated, $score){
            // 掲載順の最大値
            $maxOrder = MusicScore::maxOrder($validated['numbering_id'])->first()->order;

            // 別のカテゴリに変更する場合
            if($score->numbering_id != $validated['numbering_id'])
            {
                // 掲載順の最大値調整
                $maxOrder += 1;

                // 変更前カテゴリーの掲載順を調整
                MusicScore::sortDelete($score->numbering_id, $score->order);

                // 変更後カテゴリーの掲載順を調整
                MusicScore::sortInsert($validated['numbering_id'], $validated['order']);
            }
            else
            {
                // 同じカテゴリ内で掲載順に変更がある場合
                if($score->order !== $validated['order'])
                {
                    // 同じカテゴリーの掲載順を調整
                    MusicScore::sortUpdate($score->numbering_id, $score->order, $validated['order']);
                }
            }

            // 掲載順の最大値チェック
            if($validated['order'] > $maxOrder)
            {
                $validated['order'] = $maxOrder;
            }

            $score->update($validated);
        });

        return redirect()->route('scores.show', [$score->id])
            ->with('message', '楽譜を更新しました');
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  \App\MusicScore $score
     * @return \Illuminate\Http\Response
     */
    public function destroy(MusicScore $score)
    {
        $fileId = $score->id;

        DB::transaction(function() use($score){
            MusicScore::sortDelete($score->numbering_id, $score->order);
            $score->delete();
        });

        Storage::delete('public/' . config('const.SCORE_DIRECTORY_NAME') . '/' . $score->id . '.pdf');
        Storage::delete('public/' . config('const.MUSIC_DIRECTORY_NAME') . '/' . $score->id . '.mp3');

     return redirect()->route('scores.index')->with('message', '楽譜を削除しました');
    }
}
