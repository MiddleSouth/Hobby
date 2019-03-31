/**
 * Clearボタン：チェックボックスのクリア
 */
$('.checkClear').click(function(){
    var items = $(this).parent().prev().find('input');

    $(items).prop('checked', false);
});
