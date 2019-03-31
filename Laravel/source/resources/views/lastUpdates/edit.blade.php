@extends('layouts.common')

@section('content')
    @include('errors.form_errors')

    <div>
        現在の最終更新日：{{ $lastUpdate->last_updated_at->format('Y/m/d') }}
    </div>
 
    {!! Form::model($lastUpdate, ['method' => 'PATCH', 'route' => ['lastUpdates.update', $lastUpdate->id]]) !!}
        <div class="form-group">
        {!! Form::label('last_updated_at', '変更後の最終更新日:') !!}
        {!! Form::input('date', 'last_updated_at', \Carbon\Carbon::now()->format('Y-m-d')) !!}
        </div>
        <div class="form-group">
            {!! Form::submit('更新', ['class' => 'btn btn-primary']) !!}
            <a class="btn btn-danger" href="{{ route('dashboard') }}">キャンセル</a>
        </div>
    {!! Form::close() !!}
@endsection