<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;

class IllustRequest extends FormRequest
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
            'illust_file' => 'sometimes|required|image'
            ,'thumbnail_file' => 'sometimes|required|image'
            ,'title' => 'required'
            ,'caption' => 'present'
            ,'published_at' => 'required|date'
        ];
    }
}
