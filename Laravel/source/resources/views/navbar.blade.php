<nav class="navbar navbar-expand-lg navbar-dark bg-dark fixed-top background-dark-gray {{$addNavbarClass}}">
  <div class="container">
    <!-- ブランド表示 -->
    {{ FadeLink::getLink(route('home'), config('app.name', 'TITLE'), 'navbar-brand') }}

    <!-- スマホやタブレットで表示した時のメニューボタン -->
    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>

    <!-- メニュー -->
    <div class="collapse navbar-collapse" id="navbarSupportedContent">
      <!-- 左寄せメニュー -->
      <ul class="navbar-nav mr-auto">
          <li class="nav-item">
            {{ FadeLink::getLink(route('about'), 'About', 'nav-link') }}
          </li>
          <li class="nav-item">
            {{ FadeLink::getLink(route('illusts.index'), 'Illust', 'nav-link') }}
          </li>
          <li class="nav-item">
            <a href="https://www.nicovideo.jp/user/XXXXXXX/mylist" target="_blank" class="nav-link">Movie</a>
          </li>
          <li class="nav-item">
            {{ FadeLink::getLink(route('scores.index'), 'Music Score', 'nav-link') }}
          </li>
          <li class="nav-item">
            {{ FadeLink::getLink(route('freesoft'), 'FreeSoft', 'nav-link') }}
          </li>
          <li class="nav-item">
            <a href="http://XXXX/" target="_blank" class="nav-link">Blog</a>
          </li>
          <li class="nav-item">
            <a href="https://twitter.com/XXXX" target="_blank" class="nav-link">Twitter</a>
          </li>
      </ul>

      @auth
      <!-- 右寄せメニュー -->
      <ul class="navbar-nav">
          <!-- ログインしている時のメニュー -->
          <li class="nav-item">
            <a class="nav-link" href="{{ route('dashboard') }}">
                Dash Board
            </a>
          </li>
          <li class="nav-item list-inline">
            <a class="nav-link" href="#" onclick="event.preventDefault(); document.getElementById('logout-form').submit();">
                Logout
            </a>
            <form id="logout-form" action="{{ route('logout') }}" method="POST" style="display: none;">
                @csrf
            </form>
          </li>
      </ul>
      @endauth
    </div>
  </div>
</nav>