<?php

use Faker\Generator as Faker;
use Carbon\Carbon;

$factory->define(App\Illust::class, function (Faker $faker) {
    return [
        'category_id' => 1,
        'product_id' => 1,
        'title' => $faker->sentence(),
        'caption' => $faker->paragraph(),
        'illust_file_name' => 'test.png',
        'thumbnail_file_name' => 'test_Thumb.png',
        'published_at' => Carbon::today()
    ];
});
