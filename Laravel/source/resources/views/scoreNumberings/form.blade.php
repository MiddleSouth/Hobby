<td>{!! Form::text('name', null, ['class' => 'form-control']) !!}</td>
<td>{!! Form::text('short_name', null, ['class' => 'form-control']) !!}</td>
<td>{!! Form::select('product_id', $dropdownProducts, $productId,  ['class' => 'form-control']) !!}</td>
<td>{!! Form::number('order', null, ['class' => 'form-control small-number']) !!}</td>
<td>{!! Form::submit($buttonName, ['class' => 'btn form-control '. $buttonClass ]) !!}</td>
