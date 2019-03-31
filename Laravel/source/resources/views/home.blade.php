@extends('layouts.common')

@section('styles')
    <link href="{{ asset('css/home.css') }}" rel="stylesheet">
@endsection

@section('content')
    <!-- タブレット・スマホ向けナビゲーションバー -->
    @include('navbar', ['addNavbarClass' => 'd-lg-none'])

    {{-- PC向けの表示 --}}
    <div Class="menu-box-frame d-none d-lg-block">
        <div class="menu-box-Left background-dark-gray {{ $menuBoxLeftClass }}">
            <h1>{{ config('app.name', 'TITLE') }}</h1>
            <ul class="list-unstyled mt-5">
                <li>{!! FadeLink::getLink(route('about'), 'About') !!}</li>
                <li>{!! FadeLink::getLink(route('illusts.index'), 'Illust') !!}</li>
                <li><a href="https://www.nicovideo.jp/user/XXXXXXX/mylist" target="_blank" class="link">Movie</a></li>
                <li>{!! FadeLink::getLink(route('scores.index'), 'MusicScore') !!}</li>
                <li>{!! FadeLink::getLink(route('freesoft'), 'FreeSoft') !!}</li>
                <li><a href="http://XXXX/" target="_blank" class="link">Brog</a></li>
                <li><a href="https://twitter.com/XXXX" target="_blank" class="link">Twitter</a></li>
            </ul>
            <footer class="mt-5">
                <p>
                    Last Update : {{ $lastUpdate->format('Y/m/d') }}<br>
                    Copyright 2015<br>
                    {{ config('app.name', 'TITLE') }} all rights reserved.
                </p>
            </footer>
        </div>
        <div class="menu-box-right">
            <img src="{{ asset(Config::get('const.IMAGE_ITEM_DIRECTORY_NAME') . '/' . $imageFileName) }}">
        </div>
    </div>

    {{-- タブレット・スマホ向けの表示 --}}
    <div Class="d-lg-none text-center mt-5">
        <div>
            <img class="img-fluid" src="{{ asset(Config::get('const.IMAGE_ITEM_DIRECTORY_NAME') . '/' . $imageFileName) }}">
        </div>
        <div class="breaking-out">
            <footer class="background-dark-gray">
                <p class="py-1">
                    Last Update : {{ $lastUpdate->format('Y/m/d') }}<br>
                    Copyright 2015<br>
                    {{ config('app.name', 'TITLE') }} all rights reserved.
                </p>
            </footer>
        </div>
    </div>

@endsection
