#pragma checksum "D:\MyProgram\Hobby\RLSimulation\RLSimulation\Pages\HowToPlay.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e3d5bac5facd928151bf8063e16bbbc48be2d7da"
// <auto-generated/>
#pragma warning disable 1591
namespace RLSimulation.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#line 1 "D:\MyProgram\Hobby\RLSimulation\RLSimulation\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#line 2 "D:\MyProgram\Hobby\RLSimulation\RLSimulation\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#line 3 "D:\MyProgram\Hobby\RLSimulation\RLSimulation\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#line 4 "D:\MyProgram\Hobby\RLSimulation\RLSimulation\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#line 5 "D:\MyProgram\Hobby\RLSimulation\RLSimulation\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#line 6 "D:\MyProgram\Hobby\RLSimulation\RLSimulation\_Imports.razor"
using RLSimulation;

#line default
#line hidden
#line 7 "D:\MyProgram\Hobby\RLSimulation\RLSimulation\_Imports.razor"
using RLSimulation.Shared;

#line default
#line hidden
    [Microsoft.AspNetCore.Components.RouteAttribute("/howtoplay")]
    public partial class HowToPlay : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h3 class=\"mt-2\">概要</h3>\r\n");
            __builder.AddMarkupContent(1, "<p>\r\n\t機械学習（AI）の手法の一つである強化学習を用いて、迷路の最短経路を探索するシミュレーションです。<br>\r\n\t強化学習の特徴やメリット・デメリットを視覚的に学ぶことができます。<br>\r\n\tこのシミュレーションでは、強化学習の中でもシンプルな手法である「Q学習」により学習を行います。\r\n</p>\r\n\r\n");
            __builder.AddMarkupContent(2, "<h3 class=\"mt-2\">強化学習の特徴</h3>\r\n");
            __builder.AddMarkupContent(3, @"<p>
	強化学習は「目的」を達成するための適切な「行動」を、試行錯誤しながら学習する機械学習手法です。<br>
	このシミュレーションでは、以下の通り目的の設定と行動の学習を行います。
	<ol>
		<li>迷路の「目的」である「ゴールにたどり着くこと」に対して報酬を設定する</li>
		<li>強化学習を行うエージェント（ロボット）が実際に迷路を動き回る</li>
		<li>試行錯誤の末、なるべく多くの報酬を得られる「行動」（=最短経路）を学習する</li>
	</ol>
	エージェントには最短経路を教えていない（最短経路上に対して報酬を与えてない）にもかかわらず、最短経路を学習することができます。<br>
	ただし、最短経路を学習するまでに膨大な回数の行動（試行錯誤）が必要であるというデメリットがあります。<br>
	もっと詳しく知りたい方は、以下のサイト等を参考にしてください。
	<ul>
		<li><a href=""https://codezine.jp/article/detail/11687"" target=""_blank"">深層学習が強化学習において果たす役割とは？『現場で使える！Python深層強化学習入門』から紹介</a></li>
		<li><a href=""https://qiita.com/ikeyasu/items/67dcddce088849078b85"" target=""_blank"">趣味の強化学習入門</a></li>
		<li><a href=""https://qiita.com/qiita_kuru/items/2c00a81b4b26bf9ad210"" target=""_blank"">強化学習の基本的な考え方</a></li>
	</ul>
</p>

");
            __builder.AddMarkupContent(4, "<h3 class=\"mt-2\">シミュレーションの楽しみ方</h3>\r\n");
            __builder.AddMarkupContent(5, @"<p>
	「学習開始」ボタンを押下するとエージェントが迷路上で行動を始めます。緑色のマスがエージェントの現在地です。<br>
	はじめはランダムに行動しますが、ゴールを重ねるごとに、各マスにおいて選択すべき行動を学習していきます。マス上の矢印（↑↓→←）は、そのマスにおいてどの方向に移動すべきか学習した結果を示しています。<br>
	学習を早く進めたい場合は「スキップ」ボタンを押下することでアニメーションをスキップできます。<br>
	エージェントが最短経路でゴールに到達すると、その旨のメッセージが表示されます。<br>
	特に以下の点に注目すると、シミュレーションをより楽しめます。
	<ul>
		<li>ゴールから離れたマスにおいても、将来獲得できる報酬を期待して、選択すべき行動を学習している</li>
		<li>ある程度学習が進むまでに膨大な行動（試行錯誤）が必要である</li>
	</ul>
</p>");
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
