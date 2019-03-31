<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\LastUpdate;
use App\Http\Requests\LastUpdateRequest;

class LastUpdateController extends Controller
{
    public function __construct()
    {
        $this->middleware('auth');
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  \App\LastUpdate $lastUpdate
     * @return \Illuminate\Http\Response
     */
    public function edit(LastUpdate $lastUpdate)
    {
        return view('lastUpdates.edit', compact('lastUpdate'));
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \Illuminate\Http\Request\LastUpdateRequest  $request
     * @param  \App\LastUpdate $lastUpdate
     * @return \Illuminate\Http\Response
     */
    public function update(LastUpdateRequest $request, LastUpdate $lastUpdate)
    {
        $lastUpdate->update($request->validated());

        return redirect()->route('dashboard')->with('message', '最終更新日を更新しました');
    }
}
