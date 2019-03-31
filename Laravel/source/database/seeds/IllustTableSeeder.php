<?php

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class IllustTableSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        DB::table('illusts')->delete();
 
        factory(App\Illust::class, 10)->create();
    }
}
