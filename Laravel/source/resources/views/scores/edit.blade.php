@extends('layouts.common')

@section('styles')
    <link href="{{ asset('css/score.css') }}" rel="stylesheet">
@endsection

@section('content')

<div class="flex-box">
    <div class="show-column">
        <h1>Edit</h1>
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
        @include('errors.form_errors')
        {!! Form::model($score, ['method' => 'PATCH', 'route' => ['scores.update', $score->id]]) !!}
            @include('scores.form', ['published_at' => $score->published_at->format('Y-m-d H:i:s'), 'submitButton' => 'Edit Score'])
        {!! Form::close() !!}
    </div>

    <div class="list-column d-none d-md-block">
        @include('scores.list', ['products' => $products])
    </div>
</div>
@endsection