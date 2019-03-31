<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\LastUpdate;

class HomeController extends Controller
{
    public function index()
    {
        $menuBoxLeftClass;
        $imageFileName;
        switch(rand(1,2))
        {
            case 1:
                $menuBoxLeftClass = "padding-1";
                $imageFileName = "top1.png";
                break;
            case 2:
                $menuBoxLeftClass = "padding-2";
                $imageFileName = "top2.png";
                break;
        }

        $lastUpdate = LastUpdate::all()->first()->last_updated_at;

        return view('home', compact('menuBoxLeftClass', 'imageFileName', 'lastUpdate'));
    }
}
