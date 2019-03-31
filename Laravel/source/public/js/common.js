//グローバル変数
var toLink = "";

/**
 * ページ遷移時のフェードインアニメーション
 */
function PageFadeIn()
{
	document.getElementById("fadeIn").checked = true;
	document.getElementById("fadeBox").style.height = document.body.scrollHeight + "px";

	SetFadeOutLinkEvent();
}

/**
 * フェードアウト完了後のページ遷移イベントを登録する
 */
function SetFadeOutLinkEvent()
{
	document.getElementById("fadeBox").addEventListener("webkitTransitionEnd", function(event)
	{
		if(document.getElementById("fadeOut").checked == true)
		{
			location.href = toLink;
		}
	}, false);
}

/**
 * フェードアウトのアニメーション完了後のリンク先を指定する
 * 
 * @param {string} targetLink 
 */
function SetLink(targetLink)
{
	toLink = targetLink;
}

/**
 * ツリーの開閉
 * 
 * @param {string} parendId
 * @param {string} childId
 */
function TreeMenu(parendId, childId)
{
	var parent = document.getElementById(parendId);
	var child = document.getElementById(childId);

	parent.classList.toggle('fa-plus');
	parent.classList.toggle('fa-minus');
	child.classList.toggle('tree-open');
}
