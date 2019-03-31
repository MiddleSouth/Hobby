<div class="form-group">
    {!! Form::label('back_ground_id', '背景:', ['class' => 'control-label']) !!}
    {!! Form::select('back_ground_id', ['1' => 'white', '2' => 'black']) !!}
</div>
<div class="form-group">
    {!! Form::label('product_id', '作品名:', ['class' => 'control-label']) !!}
    {!! Form::select('product_id', $products) !!}
</div>
<div class="form-group">
    {!! Form::label('title', 'タイトル:') !!}
    {!! Form::text('title', null, ['class' => 'form-control']) !!}
</div>
<div class="form-group">
    {!! Form::label('caption', 'キャプション:') !!}
    {!! Form::textarea('caption', null, ['class' => 'form-control']) !!}
</div>
<div class="form-group">
    {!! Form::label('published_at', '公開日時（YYYY/MM/dd hh:mm:ss）:') !!}
    {!! Form::input('datetime', 'published_at', $published_at, ['class' => 'form-control']) !!}
</div>
<div class="form-group">
    {!! Form::submit($submitButton, ['class' => 'btn btn-primary form-control']) !!}
</div>
