@extends('layouts.common')

@section('styles')
    <link href="{{ asset('css/score.css') }}" rel="stylesheet">
@endsection
 
@section('content') 
 <div class="flex-box">
    <div class="show-column">
        <h1>Submit a New Score</h1>
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
        {!! Form::open(['route' => 'scores.store', 'files' => true]) !!}
            <div class="form-group">
                {!! Form::label('score_file', '楽譜pdf:', ['class' => 'control-label']) !!}
                {!! Form::file('score_file') !!}
            </div>
            <div class="form-group">
                {!! Form::label('music_file', 'MP3:', ['class' => 'control-label']) !!}
                {!! Form::file('music_file') !!}
            </div>
            @include('scores.form', ['published_at' => \Carbon\Carbon::now(), 'submitButton' => 'Add Score'])
        {!! Form::close() !!}
    </div>

    <div class="list-column d-none d-md-block">
        @include('scores.list', ['products' => $products])
    </div>
</div>
@endsection