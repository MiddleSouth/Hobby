@extends('layouts.common')

@section('scripts')
    <script src="{{ asset('js/illust.js') }}" defer></script>
@endsection

@section('styles')
    <link href="{{ asset('css/illust.css') }}" rel="stylesheet">
@endsection

@section('content')
    <div class="index-container">
        @auth
            <div>
                <a href="{{ route('illusts.create') }}" class="btn btn-success mb-2">New Create</a>
            </div>
        @endauth

        <!-- 検索 -->
        <button type="button" class="btn btn-primary mb-2" data-toggle="collapse" data-target="#collapseSearch" aria-expanded="false" aria-controls="collapseSearch">
            Search Box
        </button>
        <div class="collapse" id="collapseSearch">
            <div class="item-box background-sky">
                {!! Form::open(['route' => 'illusts.search']) !!}
                    <div class="form-group">
                    @foreach($searchConditions as $category)
                        <h1 class="font-script">{{ $category->name }}</h1>
                        <div class="ml-3">
                        @foreach($category->illustProducts as $product)
                            {{-- チェックボックスの装飾のため、ファサードを利用せずにHTMLを直書きする --}}
                            <label>
                                <input type="checkbox" name="products[]" class="check-input" value="{{ $product->id }}"
                                    {{ in_array($product->id, $searchProductIds)? 'checked="checked"' : '' }}>
                                <span class="check-parts font-script">{{ $product->name }}</span>
                            </label>
                        @endforeach
                        </div>
                    @endforeach
                    </div>
                    <div class="form-group">
                        {!! Form::submit('Search', ['class' => 'btn btn-primary']) !!}
                        <input type="button" value="Check Clear" class="checkClear btn btn-danger ml-2">
                    </div>
                {!! Form::close() !!}
            </div>
        </div>

        @if(count($searchProductIds) === 0)
            <h1 class="font-script mt-4">New Arrivals</h1>
            <div class="item-box background-gray-red">
                <h2 class="font-script">Last Update : {{ $newIllusts->first()->published_at->format('Y/m/d') }}</h2>
                <div class="illust-thumbnail-box">
                    {{-- リストタグとリストタグの間に改行があると不要なスペースが入るため、改行部分をコメントアウトする --}}
                    <ul class="list-inline"><!--
                    @foreach($newIllusts as $newIllust)
                        --><li class="list-inline-item mb-4 mx-3">
                            {{ FadeLink::getImageLink(
                                route('illusts.show', $newIllust->id),
                                asset('storage/' . Config::get('const.ILLUSTS_DIRECTORY_NAME')) . '/' . $newIllust->thumbnail_file_name,
                                $newIllust->title,
                                'thumbnail'
                            ) }}
                        </li><!--
                    @endforeach
                    --></ul>
                </div>
            </div>
        @endif

        @foreach($searchedIllusts as $category)
            <h1 class="font-script mt-4">{{ $category->name }}</h1>
            <div class="item-box background-gray">
            @foreach($category->illustProducts as $product)
                <h2 class="font-script">{{ $product->name }}</h2>
                <div class="illust-thumbnail-box">
                {{-- リストタグとリストタグの間に改行があると不要なスペースが入るため、改行部分をコメントアウトする --}}
                <ul class="list-inline"><!--
                @foreach($product->illusts as $illust)
                    --><li class="list-inline-item mb-4 mx-3">
                        {{ FadeLink::getImageLink(
                            route('illusts.show', $illust->id),
                            asset('storage/' . Config::get('const.ILLUSTS_DIRECTORY_NAME')) . '/' . $illust->thumbnail_file_name,
                            $illust->title,
                            'thumbnail'
                            ) }}
                    </li><!--
                @endforeach
                --></ul>
                </div>
            @endforeach
            </div>
        @endforeach
    </div>
    <div class="preload-background background-sky"></div>
@endsection
