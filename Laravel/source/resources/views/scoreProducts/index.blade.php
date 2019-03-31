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
            {!! Form::open(['route' => 'scoreProducts.store']) !!}
                @include('scoreProducts.form',[
                    'buttonName' => '登録'
                    ,'buttonClass' => 'btn-success'
                    ])
            {!! Form::close() !!}
        </tr>
    <table>

    <h2 class="mt-4">Edit Product</h2>
    <table class="ml-4 form-table">
    @foreach($products as $product)
        <tr>
            {!! Form::model($product, ['method' => 'PATCH', 'route' => ['scoreProducts.update', $product->id]]) !!}
                @include('scoreProducts.form',[
                    'buttonName' => '更新'
                    ,'buttonClass' => 'btn-primary'
                    ])
            {!! Form::close() !!}
            <td>
                @if( $product->numbering_count == 0 )
                    {!! delete_form(['scoreProducts', $product->id]) !!}
                @endif
            </td>
        </tr>
    @endforeach
    <table>

@endsection
