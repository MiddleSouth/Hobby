@foreach($products as $product)
    <div class="mb-4">
        <h4 class="font-weight-bold">{{ $product->name }}</h4>
        @foreach($product->musicScoreProductNumberings as $numbering)
            @if(!is_null($numbering->short_name))
            <span class="font-weight-bold">{{ $numbering->short_name }}</span>
            @endif
            <table class="ml-2">
                @foreach($numbering->musicScores as $score)
                <tr>
                    <td>{{ $score->order }}：</td>
                    <td>{{ $score->title }}</td>
                </tr>
                @endforeach
            </table>
        @endforeach
    </div>
@endforeach
