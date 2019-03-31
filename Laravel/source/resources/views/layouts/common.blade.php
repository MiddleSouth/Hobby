<!DOCTYPE HTML>
<html lang="ja">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <!-- CSRF Token -->
    <meta name="csrf-token" content="{{ csrf_token() }}">

    <title>{{ config('app.name', 'TITLE') }}</title>

    <meta name="description" content="ゲーム、漫画のイラストを書いたり、ゲームのピアノ楽譜を作ったり、ゲームの動画を投稿しているサイト。ゲーム大好き。">

    <!-- IE互換設定 -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge">

    <!-- Scripts -->
    <script src="{{ asset('js/app.js') }}" defer></script>
    <script src="{{ asset('js/common.js') }}" defer></script>
    @yield('scripts')

    <!-- Fonts -->
    <link rel="dns-prefetch" href="//fonts.gstatic.com">
    <link href="https://fonts.googleapis.com/css?family=Charm" rel="stylesheet" type="text/css">

    <!-- Awesome -->
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/solid.css" integrity="sha384-r/k8YTFqmlOaqRkZuSiE9trsrDXkh07mRaoGBMoDcmA58OHILZPsk29i2BsFng1B" crossorigin="anonymous">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/fontawesome.css" integrity="sha384-4aon80D8rXCGx9ayDt85LbyUHeMWd3UiBaWliBlJ53yzm9hqN21A+o1pqoyK04h+" crossorigin="anonymous">

    <!-- favicon -->
    <link rel="icon" href="{{ asset('favicon.ico') }}">

    <!-- Styles -->
    <link href="{{ asset('css/app.css') }}" rel="stylesheet">
    <link href="{{ asset('css/common.css') }}" rel="stylesheet">

    @yield('styles')

</head>
<body onLoad="PageFadeIn();">

    <!--フェードを制御するラジオボタン-->
    <input type="radio" name="fade" id="fadeIn">
    <input type="radio" name="fade" id="fadeOut">
    <!-- フェード時に画面を覆う隠し要素 -->
    <div id="fadeBox">
    </div>

    @if(\Route::current()->getName() !== 'home')
        <!-- ナビゲーションバー -->
        @include('navbar', ['addNavbarClass' => ''])
    @endif

    @if (session('message'))
        <!-- フラッシュメッセージ -->
        <div class="alert alert-success">{{ session('message') }}</div>
    @endif
 
    <!-- コンテンツ表示領域 -->
    <div class="container pb-4">
        @yield('content')
 
        @if(Route::current()->getName() !== 'home')
            <!-- フッター -->
            <footer>
                <p class="footer">
                    copyright 2015<br>{{ config('app.name', 'TITLE') }} all rights reserved.
                </p>
            </footer>
        @endif
    </div>

</body>
</html>