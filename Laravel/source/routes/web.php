<?php

// 静的ページ
Route::get('/', 'HomeController@index')->name('home');
Route::get('about', 'PagesController@about')->name('about');
Route::get('freesoft', 'PagesController@freesoft')->name('freesoft');

// イラストページ
Route::post('illusts/search', 'IllustsController@search')->name('illusts.search');
Route::resource('illusts', 'IllustsController');

// 楽譜ページ
Route::resource('scores', 'MusicScoreController');

// 最終更新日の管理ページ
Route::resource('lastUpdates', 'LastUpdateController')->only(['edit','update']);

// イラストの作品マスタの管理ページ
Route::resource('illustProducts', 'IllustProductsController')
    ->only(['index','store','update','destroy']);

// 楽譜のナンバリングマスタの管理ページ
Route::resource('scoreNumberings', 'MusicScoreProductNumberingsController')
    ->only(['index','store','update','destroy']);

// 楽譜の作品マスタの管理ページ
Route::resource('scoreProducts', 'MusicScoreProductsController')
    ->only(['index','store','update','destroy']);

// ログイン後のダッシュボード
Auth::routes();
Route::get('/dashboard', 'DashboardController@index')->name('dashboard');

// 旧サイト構成からのリダイレクト
Route::redirect('/index.html', '/');
Route::redirect('/contents/about.html', '/about');
Route::redirect('/contents/illust.html', '/illusts');
Route::redirect('/contents/link.html', '/');
Route::redirect('/contents/contact.html', '/');
Route::redirect('/contents/movie.html', '/');
Route::redirect('/contents/music_score.html', '/scores');
Route::redirect('/contents/works.html', '/');
Route::redirect('/contents/freesoft/langtons_ant.html', '/freesoft');
