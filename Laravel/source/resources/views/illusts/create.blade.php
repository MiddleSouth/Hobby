@extends('layouts.common')
 
@section('content')
    <h1>Submit a New Illust</h1>
 
    <hr/>

    @if ($errors->any())
        <div class="alert alert-danger">
            <ul>
                @foreach ($errors->all() as $error)
                    <li>{{ $error }}</li>
                @endforeach
            </ul>
        </div>
    @endif
 
    {!! Form::open(['route' => 'illusts.store', 'files' => true]) !!}
        <div class="form-group">
            {!! Form::label('illust_file', '画像ファイル:', ['class' => 'control-label']) !!}
            {!! Form::file('illust_file') !!}
        </div>
        <div class="form-group">
            {!! Form::label('thumbnail_file', 'サムネイルファイル:', ['class' => 'control-label']) !!}
            {!! Form::file('thumbnail_file') !!}
        </div>
        @include('illusts.form', ['published_at' => \Carbon\Carbon::now(), 'submitButton' => 'Add Illust'])
    {!! Form::close() !!}
@endsection