@extends('layouts.common')

@section('styles')
    <link href="{{ asset('css/freesoft.css') }}" rel="stylesheet">
@endsection

@section('content')

<div class="box">
    <h1 class="font-script">ラングトンのアリ</h1>

    <figure>
        <img class="img-fluid" alt="ラングトンのアリ" src="{{ asset(Config::get('const.IMAGE_ITEM_DIRECTORY_NAME') . '/langtonsant.png') }}">
    </figure>

    <a class="btn btn-primary" role="button" target="_blank" href="{{ asset('storage/' . Config::get('const.ZIP_DIRECTORY_NAME') . '/LangtonsAnt_Ver1.00.zip') }}">ダウンロード(Ver1.00)</a>

    <h2 class="mt-4">ラングトンのアリとは</h2>
    <blockquote class="blockquote-area background-sky" cite="https://ja.wikipedia.org/wiki/%E3%83%A9%E3%83%B3%E3%82%B0%E3%83%88%E3%83%B3%E3%81%AE%E3%82%A2%E3%83%AA">
        <p>
            平面が格子状に構成され、各マスが白または黒で塗られる。ここで1つのマスを「アリ」とする。
            アリは各ステップで上下左右のいずれかのマスに移動することができる。<br>
            アリは以下の規則に従って移動する。
        </p>
        <ul class="list-unstyled ml-3">
            <li>黒いマスにアリがいた場合、90°右に方向転換し、そのマスの色を反転させ、1マス前進する。</li>
            <li>白いマスにアリがいた場合、90°左に方向転換し、そのマスの色を反転させ、1マス前進する。</li>
        </ul>
        <p>
            この単純な規則で驚くほど複雑な動作をする。
        </p>
        <div class="text-right">
            -wikipedia より
        </div>
    </blockquote>

    <h2 class="mt-4">何ができるの？</h2>
    <p>
        ラングトンのアリが動く様子を「スクリーンセーバ」と「シミュレーション」で楽しめます。
    </p>

    <h2 class="mt-4">遊び方</h2>
    <p>
        このページ上部の画像下にあるダウンロードボタンをクリックして、ラングトンのアリをダウンロードします。<br>
        ダウンロードしたzipファイルを解凍すると、以下のファイルが入っています。
    </p>

    <ul class="d-none d-sm-block">
        <li class="file-list">
            <span class="font-weight-bold">Install.exe</span>
            <span class="file-list-description">スクリーンセーバのインストーラ</span>
        </li>
        <li class="file-list">
            <span class="font-weight-bold">LangtonsAnt.exe</span>
            <span class="file-list-description">本体</span>
        </li>
        <li class="file-list">
            <span class="font-weight-bold">ReadMe.txt</span>
            <span class="file-list-description">説明書</span>
        </li>
        <li class="file-list">
            <span class="font-weight-bold">UnInstall.exe</span>
            <span class="file-list-description">スクリーンセーバのアンインストーラ</span>
        </li>
    </ul>

    <ul class="d-sm-none">
        <li>
            <span class="font-weight-bold">Install.exe</span><br>
            <span class="pl-2">スクリーンセーバのインストーラ</span>
        </li>
        <li>
            <span class="font-weight-bold">LangtonsAnt.exe</span><br>
            <span class="pl-2">本体</span>
        </li>
        <li>
            <span class="font-weight-bold">ReadMe.txt</span><br>
            <span class="pl-2">説明書</span>
        </li>
        <li>
            <span class="font-weight-bold">UnInstall.exe</span><br>
            <span class="pl-2">スクリーンセーバのアンインストーラ</span>
        </li>
    </ul>

    <p class="mt-3">
        説明書(ReadMe.txt)を読んで遊んでください。<br>
        以上！
    </p>
</div>
@endsection