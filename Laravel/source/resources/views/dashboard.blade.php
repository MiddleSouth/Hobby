@extends('layouts.common')

@section('content')

    @if (session('status'))
        <div class="alert alert-success" role="alert">
            {{ session('status') }}
        </div>
    @endif

    <h1>Dash Board</h1>
    <ul class="list-unstyled">
        <li><a class="link" href="{{ route('lastUpdates.edit', ['id' => '0']) }}">最終更新日管理</a></li>
        <li><a class="link" href="{{ route('illustProducts.index') }}">イラスト-作品マスタ管理</a></li>
        <li><a class="link" href="{{ route('scoreNumberings.index') }}">楽譜-ナンバリングマスタ管理</a></li>
        <li><a class="link" href="{{ route('scoreProducts.index') }}">楽譜-作品マスタ管理</a></li>
    </ul>

@endsection
