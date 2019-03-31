<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class CreateMusicScoresTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('music_scores', function (Blueprint $table) {
            $table->increments('id');
            $table->unsignedInteger('numbering_id');
            $table->string('nico_movie_id');
            $table->string('title');
            $table->text('description')->nullable();
            $table->integer('order');
            $table->datetime('published_at');
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('music_scores');
    }
}
