<?php

namespace App\Providers;

use Illuminate\Support\ServiceProvider;

class FadeLinkServiceProvider extends ServiceProvider
{
    /**
     * Bootstrap services.
     *
     * @return void
     */
    public function boot()
    {
        //
    }

    /**
     * Register services.
     *
     * @return void
     */
    public function register()
    {
        $this->app->bind(
            'fadelink',
            'App\Services\FadeLink'
        );
    }
}
