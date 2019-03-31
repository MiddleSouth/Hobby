<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class LastUpdate extends Model
{
    public $timestamps = false;

    protected $fillable = ['last_updated_at'];

    protected $dates = ['last_updated_at'];
}
