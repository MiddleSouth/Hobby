<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;

class MusicScoreRequest extends FormRequest
{
    /**
     * Determine if the user is authorized to make this request.
     *
     * @return bool
     */
    public function authorize()
    {
        return true;
    }

    /**
     * Get the validation rules that apply to the request.
     *
     * @return array
     */
    public function rules()
    {
        return [
            'score_file' => 'sometimes|required|mimes:pdf'
            // TODO:mp3のバリデーションを自作する（Laravelデフォルトのバリデーションでは、フリーソフトで作成したmp3ファイルは弾かれる）
            ,'music_file' => 'sometimes|required'
            ,'numbering_id' => 'required'
            ,'nico_movie_id' => 'required'
            ,'title' => 'required'
            ,'description' => 'present'
            ,'order' => 'required|numeric|min:1'
            ,'published_at' => 'required|date'
        ];
    }
}
