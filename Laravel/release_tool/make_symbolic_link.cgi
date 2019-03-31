#!/usr/bin/perl
use utf8;
use CGI;
use File::Find;
use Cwd;

my $cgi = new CGI;
my $target = $cgi->param('target');
my $link = $cgi->param('link');
our $msg='';

if($target ne '' && $link ne ''){
  $msg.="$target のシンボリックリンク($link)を生成します。";
  if( (symlink $target,$link) == 0){
    $msg.="失敗しました。";
  }else{
    $msg.="成功しました。";
  }
}

print $cgi->header(
  -type=>'text/html',
  -expires=>'0',
  -charset=>'utf-8'
);

print << "EOF";
<!DOCTYPE html>
<html lang="jp">
<head>
<meta charset="utf-8" />
<!-- <link reil="stylesheet"  href="style.css"> -->
<style>
ul{
  list-style: none;
}
label{
  display:block;
  float:left;
  width :7em;
}

</style>


<title>シンボリックリンクの設定</title>
<script src="https://code.jquery.com/jquery-3.3.1.min.js"
        integrity="sha256-FgpCb/KJQlLNfOu91ta32o/NMZxltwRo8QtmkMRdAu8="
 crossorigin="anonymous"></script>
</head>
<body>
<main>
  <h1>シンボリックリンクの設定</h1>
        <div>$msg</div>
        <form>
        <ul>
        <li><label for="target">ターゲット:</label><input name="target" size=100 id="target"></li>
        <li><label for="link">リンク先:</label><input name="link" id="link" size=100  id="link"></li>
        <li><input type="submit" value="実行"><input type="reset" value="クリア"></li>
        </ul>
        </form>
</main>
<footer>
<hr>
引用元:
<a href="https://qiita.com/items/e99d1f3db7de280de36d">https://qiita.com/items/e99d1f3db7de280de36d</a>
</footer>
</body>
</html>
EOF
