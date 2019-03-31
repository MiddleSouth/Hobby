@extends('layouts.common')

@section('content')

    @if ($errors->any())
        <div class="alert alert-danger">
            <ul>
                @foreach ($errors->all() as $error)
                    <li>{{ $error }}</li>
                @endforeach
            </ul>
        </div>
    @endif

    <a href="{{ route('dashboard') }}" class="btn btn-primary">戻る</a><br>
    <h2 class="mt-4">Create Numbering</h2>
    <table class="ml-4 form-table">
        <tr>
            {!! Form::open(['route' => 'scoreNumberings.store']) !!}
                @include('scoreNumberings.form',[
                    'productId' => 0
                    , 'buttonName' => '登録'
                    , 'buttonClass' => 'btn-success'
                    ])
            {!! Form::close() !!}
        </tr>
    <table>

    @foreach($products as $product)
    <h2 class="mt-4">{{ $product->name }}</h2>
    <table class="ml-4 form-table">
        @foreach($product->musicScoreProductNumberings as $numbering)
        <tr>
            {!! Form::model($numbering, ['method' => 'PATCH', 'route' => ['scoreNumberings.update', $numbering->id]]) !!}
                @include('scoreNumberings.form',[
                    'productId' => $numbering->product_id
                    , 'buttonName' => '更新'
                    , 'buttonClass' => 'btn-primary'
                    ])
            {!! Form::close() !!}
            <td>
                @if( $numbering->score_count == 0 )
                    {!! delete_form(['scoreNumberings', $numbering->id]) !!}
                @endif
            </td>
        </tr>
        @endforeach
    <table>
    @endforeach

@endsection
