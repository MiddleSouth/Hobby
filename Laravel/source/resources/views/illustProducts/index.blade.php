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
    <h2 class="mt-4">Create Product</h2>
    <table class="ml-4 form-table">
        <tr>
            {!! Form::open(['route' => 'illustProducts.store']) !!}
                @include('illustProducts.form',[
                    'categoryId' => 0
                    , 'buttonName' => '登録'
                    , 'buttonClass' => 'btn-success'
                    ])
            {!! Form::close() !!}
        </tr>
    <table>

    @foreach($categories as $category)
    <h2 class="mt-4">{{ $category->name }}</h2>
    <table class="ml-4 form-table">
        @foreach($category->illustProducts as $product)
        <tr>
            {!! Form::model($product, ['method' => 'PATCH', 'route' => ['illustProducts.update', $product->id]]) !!}
                @include('illustProducts.form',[
                    'categoryId' => $product->category_id
                    , 'buttonName' => '更新'
                    , 'buttonClass' => 'btn-primary'
                    ])
            {!! Form::close() !!}
            <td>
                @if( $product->illust_count == 0 )
                    {!! delete_form(['illustProducts', $product->id]) !!}
                @endif
            </td>
        </tr>
        @endforeach
    <table>
    @endforeach

@endsection
