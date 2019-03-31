<td>{!! Form::text('name', null, ['class' => 'form-control']) !!}</td>
<td>{!! Form::select('category_id', $dropdownCategories, $categoryId,  ['class' => 'form-control']) !!}</td>
<td>{!! Form::number('order', null, ['class' => 'form-control small-number']) !!}</td>
<td>{!! Form::submit($buttonName, ['class' => 'btn form-control '. $buttonClass ]) !!}</td>
