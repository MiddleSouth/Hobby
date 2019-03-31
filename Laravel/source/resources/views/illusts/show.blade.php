@extends('layouts.common')

@section('styles')
    <link href="{{ asset('css/illust.css') }}" rel="stylesheet">
    @if($illust->back_ground_id === 2)
        <link href="{{ asset('css/illust_show_black.css') }}" rel="stylesheet">
    @endif
@endsection
 
@section('content')
    @auth
        <div class="mb-3">
            <a href="{{ route('illusts.edit', ['id' => $illust->id]) }}" class="btn btn-primary">
                編集
            </a>

            {!! delete_form(['illusts', $illust->id]) !!}
        </div>
    @endauth

    <!-- イラスト表示領域 -->
    <article class="mb-5">
        <figure>
            <div class="illust-show">
                <img class="illust-main" src = "{{ asset('storage/' . Config::get('const.ILLUSTS_DIRECTORY_NAME')) . '/' . $illust->illust_file_name }}"><br>
            </div>
            <figcaption class="mt-3 text-center">
                <h1 class="font-script illust-title">- {{ $illust->title }} -</h1>
                <div>{!! nl2br(e($illust->caption)) !!}</div>
            </figcaption>
        </figure>
    </article>
 
    <!-- 前後イラストのサムネイル表示領域 -->
    <div>
        <div class="float-left">
            @if($nextIllust !== null)
                {{ FadeLink::getLinkStartTag(route('illusts.show', $nextIllust->id)) }}
                    <img class="thumbnail" src="{{ asset('storage/' . Config::get('const.ILLUSTS_DIRECTORY_NAME')) . '/' . $nextIllust->thumbnail_file_name }}" alt="{{ $nextIllust->title }}"><br>
                    <span class='font-script'>&#060;&#060;Next</span>
                {{ FadeLink::getLinkEndTag() }}
            @endif
        </div>
        <div class="float-right text-right">
            @if($prevIllust !== null)
                {{ FadeLink::getLinkStartTag(route('illusts.show', $prevIllust->id)) }}
                    <img class="thumbnail" src="{{ asset('storage/' . Config::get('const.ILLUSTS_DIRECTORY_NAME')) . '/' . $prevIllust->thumbnail_file_name }}" alt="{{ $prevIllust->title }}"><br>
                    <span class='font-script'>Prev&#062;&#062;</span>
                {{ FadeLink::getLinkEndTag() }}
            @endif
        </div>
    </div>

    <!-- イラスト一覧に戻る -->
    <div class="float-clear w-100 text-center">
        {{ FadeLink::getLink(route('illusts.index'), 'Back To Illustration List', 'link font-script back-to-list') }}
    </div>

@endsection
