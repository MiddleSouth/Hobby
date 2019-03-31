@extends('layouts.common')

@section('styles')
    <link href="{{ asset('css/score.css') }}" rel="stylesheet">
@endsection

@section('content')
    @auth
        <a href="{{ route('scores.create') }}" class="btn btn-success">登録</a>
        <a href="{{ route('scores.edit', ['id' => $showScore->id]) }}" class="btn btn-primary">編集</a>
        {!! delete_form(['scores', $showScore->id]) !!}
    @endauth

    <div class="flex-box">
        <div class="show-column">
            <h3>{{ $showNumbering->name }}</h3>
            <h1>{{ $showScore->title }}</h1>
            <div class="mt-4"><script type="application/javascript" src="https://embed.nicovideo.jp/watch/{{ $showScore->nico_movie_id }}/script?w=640&h=360"></script><noscript><a href="https://www.nicovideo.jp/watch/{{ $showScore->nico_movie_id }}">【ピアノ楽譜付き】パワポケ6　しあわせ島　後編【弾いてみた】</a></noscript></div>
            <div class="published-box">
                公開日:{{ $showScore->published_at->format('Y/m/d') }}
            </div>
            <h3 class="mt-3">Download</h3>
            <a class="btn btn-primary px-2" download="{{ $showNumbering->short_name . $showScore->title }}.pdf" href="{{ asset('storage/' . Config::get('const.SCORE_DIRECTORY_NAME')) . '/' . $showScore->id }}.pdf">
                楽譜(PDF)
            </a>
            <a class="btn btn-primary px-4 ml-1" download="{{ $showNumbering->short_name . $showScore->title }}.mp3" href="{{ asset('storage/' . Config::get('const.MUSIC_DIRECTORY_NAME')) . '/' . $showScore->id }}.mp3">
                MP3
            </a>

            @if( !empty($showScore->description) )
                <div class="background-gray mt-4 description-box">{!! nl2br(e($showScore->description)) !!}</div>
            @endif
        </div>

        <div class="menu-column d-none d-md-block">
            @include('scores.menu', ['products' => $products,'menu_number' => '1'])
        </div>
    </div>

    <div class="d-md-none mt-5">
        @include('scores.menu', ['products' => $products,'menu_number' => '2'])
    </div>

@endsection
