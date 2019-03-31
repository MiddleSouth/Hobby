<div class="form-group">
    {!! Form::label('numbering_id', '作品名:', ['class' => 'control-label']) !!}
    {!! Form::select('numbering_id', $numberings) !!}
</div>
<div class="form-group">
    {!! Form::label('nico_movie_id', 'ニコニコ動画ID:') !!}
    {!! Form::text('nico_movie_id', null, ['class' => 'form-control']) !!}
</div>
<div class="form-group">
    {!! Form::label('title', 'タイトル:') !!}
    {!! Form::text('title', null, ['class' => 'form-control']) !!}
</div>
<div class="form-group">
    {!! Form::label('description', '説明:') !!}
    {!! Form::textarea('description', null, ['class' => 'form-control']) !!}
</div>
<div class="form-group">
    {!! Form::label('order', '掲載順:') !!}
    {!! Form::number('order', null) !!}
</div>
<div class="form-group">
    {!! Form::label('published_at', '公開日時（YYYY/MM/dd hh:mm:ss）:') !!}
    {!! Form::input('datetime', 'published_at', $published_at, ['class' => 'form-control']) !!}
</div>
<div class="form-group">
    {!! Form::submit($submitButton, ['class' => 'btn btn-primary form-control']) !!}
</div>
