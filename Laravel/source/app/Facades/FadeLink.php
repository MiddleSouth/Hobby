<?php
namespace App\facades;

use Illuminate\Support\Facades\Facade;

class FadeLink extends Facade{
    protected static function getFacadeAccessor(){
        return 'fadelink';
    }
}
