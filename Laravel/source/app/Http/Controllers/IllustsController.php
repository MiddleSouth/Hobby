<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\Illust;
use App\IllustCategory;
use App\IllustProduct;
use App\Http\Requests\IllustRequest;
use Illuminate\Support\Collection;
use Illuminate\Support\Facades\Storage;

class IllustsController extends Controller
{
    public function __construct()
    {
        $this->middleware('auth')
            ->except(['index', 'show', 'search']);
    }

    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index(Request $request)
    {
        // 検索条件が設定されているか確認
        $searchProductIds;
        if($request->has('products'))
        {
            $searchProductIds = $request->input('products');
        }
        else
        {
            // 検索条件なしの場合は空の配列を定義する
            $searchProductIds = [];
        }

        $newIllusts = Illust::newArrivals(4)->get();
        $searchConditions = IllustCategory::searchConditions()->get();
        $searchedIllusts = IllustCategory::searchedCategories($searchProductIds)->get();

        return view('illusts.index', compact('newIllusts', 'searchConditions', 'searchProductIds', 'searchedIllusts'));
    }

    /**
     * 検索したイラスト一覧を表示
     *
     * @param  \Illuminate\Http\Request  $request
     * @return \Illuminate\Http\Response
     */
    public function search(Request $request)
    {
        return $this->index($request);
    }

    /**
     * Show the form for creating a new resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function create()
    {
        $products = IllustProduct::basicOrder()->pluck('name', 'id');

        return view('illusts.create', compact('products'));
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  \App\Http\Requests\IllustRequest  $request
     * @return \Illuminate\Http\Response
     */
    public function store(IllustRequest $request)
    {
        // イラストファイル以外の入力値をそのまま反映する
        $inputs = $request->only(['title','caption','product_id','back_ground_id','published_at']);

        // イラストファイルの取得・保存
        if ($request->file('illust_file')->isValid()
            && $request->file('thumbnail_file')->isValid())
        {
            // イラストファイルをサーバーに保存
            $illustFileName = $request->illust_file->store('public/' . config('const.ILLUSTS_DIRECTORY_NAME'));
            $thumbFileName = $request->thumbnail_file->store('public/' . config('const.ILLUSTS_DIRECTORY_NAME'));

            // DBにはファイル名を保存
            $inputs['illust_file_name'] = basename($illustFileName);
            $inputs['thumbnail_file_name'] = basename($thumbFileName);
        }
        else
        {
            return redirect()->back()->withInput()->withErrors(['file' => '画像ファイルがアップロードされていない、または不正なデータです。']);
        }

        Illust::create($inputs);

        return redirect()->route('illusts.index')->with('message', 'イラストを追加しました');
    }

    /**
     * Display the specified resource.
     *
     * @param  \App\Illust  $illust
     * @return \Illuminate\Http\Response
     */
    public function show(Illust $illust)
    {
        $nextIllust = Illust::prevOrNextIllust($illust->id, true)->first();
        $prevIllust = Illust::prevOrNextIllust($illust->id, false)->first();

        return view('illusts.show', compact('illust', 'nextIllust', 'prevIllust'));
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  \App\Illust  $illust
     * @return \Illuminate\Http\Response
     */
    public function edit(Illust $illust)
    {
        $products = IllustProduct::basicOrder()->pluck('name', 'id');

        return view('illusts.edit', compact('illust', 'products'));
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \App\Http\Requests\IllustRequest  $request
     * @param  \App\Illust  $illust
     * @return \Illuminate\Http\Response
     */
    public function update(IllustRequest $request, Illust $illust)
    {
        // イラストファイル以外の入力値をそのまま反映する
        $inputs = $request->only(['title','caption','product_id','back_ground_id','published_at']);

        // ファイル名は変更しない
        $inputs['illust_file_name'] = $illust->illust_file_name;
        $inputs['thumbnail_file_name'] = $illust->thumbnail_file_name;

        $illust->fill($inputs)->save();
 
        return redirect()->route('illusts.show', [$illust->id])
            ->with('message', 'イラストを更新しました');
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  \App\Illust  $illust
     * @return \Illuminate\Http\Response
     */
    public function destroy(Illust $illust)
    {
        Storage::delete('public/' . config('const.ILLUSTS_DIRECTORY_NAME') . '/' . $illust->illust_file_name);
        Storage::delete('public/' . config('const.ILLUSTS_DIRECTORY_NAME') . '/' . $illust->thumbnail_file_name);
        $illust->delete();
 
        return redirect()->route('illusts.index')->with('message', 'イラストを削除しました');
    }
}
