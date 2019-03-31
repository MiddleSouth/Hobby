<?php

namespace App\Services;

use Illuminate\Support\HtmlString;

/**
 * フェード機能付きのリンクを取得する
 */
class FadeLink {

    /**
     * フェード機能付きのリンクを取得する
     * 
     * @param string $url
     * @param string $text
     * @param string $class デフォルト値：link
     *
     * @return \Illuminate\Support\HtmlString
     */
    public function getLink($url, $text, $class = 'link')
    {
        return new HtmlString($this->getLinkStartTag($url, $class) . $text . $this->getLinkEndTag());
    }

    /**
     * フェード機能付きの画像リンクを取得する
     * 
     * @param string $url
     * @param string $imgUrl
     * @param string $imgTitle
     * @param string $imgClass
     * @param string $linkClass デフォルト値：link
     *
     * @return \Illuminate\Support\HtmlString
     */
    public function getImageLink($url, $imgUrl, $imgTitle, $imgClass, $linkClass = 'link')
    {
        $text = "<img class =\"$imgClass\" src=\"$imgUrl\" alt=\"$imgTitle\">";
        return $this->getLink($url, $text, $linkClass);
    }

    /**
     * フェード機能付きリンクの開始タグを取得する
     * @param string $url
     * @param string $class デフォルト値：link
     *
     * @return \Illuminate\Support\HtmlString
     */
    public function getLinkStartTag($url, $class = 'link')
    {
        return new HtmlString("<label onclick=\"SetLink('$url')\" class=\"$class\" for=\"fadeOut\">");
    }

    /**
     * フェード機能付きリンクの終了タグを取得する
     * 
     * @return \Illuminate\Support\HtmlString
     */
    public function getLinkEndTag()
    {
        return new HtmlString("</label>");
    }

}