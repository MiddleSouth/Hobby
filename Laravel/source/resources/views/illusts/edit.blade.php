@extends('layouts.common')

@section('styles')
    <link href="{{ asset('css/illust.css') }}" rel="stylesheet">
@endsection

@section('content')
    <h1>Edit</h1>
 
    <hr/>
    <div class="mb-3">
        <img src="{{ asset('storage/' . Config::get('const.ILLUSTS_DIRECTORY_NAME')) . '/' . $illust->illust_file_name }}" class="preview-illust">
    </div>
    @include('errors.form_errors')
 
    {!! Form::model($illust, ['method' => 'PATCH', 'route' => ['illusts.update', $illust->id]]) !!}
        @include('illusts.form', ['published_at' => $illust->published_at->format('Y-m-d H:i:s'), 'submitButton' => 'Edit Illust'])
    {!! Form::close() !!}
@endsection