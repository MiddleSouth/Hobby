<h3>Menu</h3>
@foreach($products as $product)
<span class="font-weight-bold">{{ $product->name }}</span>
<ul class="list-unstyled">
    @foreach($product->musicScoreProductNumberings as $numbering)
    <li>
        @if(!is_null($numbering->short_name))
        {{-- ナンバリングタイトルが存在する場合は、ツリーメニューとして描画 --}}
        <a class="tree-parent" href="javaScript:TreeMenu('parent_{{ $menu_number . '_' . $numbering->id }}', 'child_{{ $menu_number . '_' . $numbering->id }}')">
            <span class="ml-1"><i class="fas fa-plus" id="parent_{{ $menu_number . '_' . $numbering->id }}"></i>{{ $numbering->short_name }}</span>
        </a>
        <ul class="list-unstyled tree-item ml-3" id="child_{{ $menu_number . '_' . $numbering->id }}">
        @else
        {{-- ナンバリングタイトルが存在しない場合は、マージンのみ調整 --}}
        <ul class="list-unstyled ml-3">
        @endif
            @foreach($numbering->musicScores as $score)
            <li>
                <a href="{{ route('scores.show', $score->id) }}">{{ $score->title }}</a>
            </li>
            @endforeach
        </ul>
    </li>
    @endforeach
</ul>
@endforeach
