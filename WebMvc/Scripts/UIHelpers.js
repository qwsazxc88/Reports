var UIHelpers = (function () {
    return {

        // Обработка смены состояний переключателей
        handleCheckboxState: function ($checkbox, checkedActionObject, uncheckedActionObject, actOnPageLoad) {
            var handler = function () {
                if ($checkbox.attr("checked") === true) {
                    checkedActionObject.act();
                }
                else {
                    uncheckedActionObject.act();
                }
            }
            $checkbox.bind("click", handler);

            if (actOnPageLoad === true) {
                handler();
            }
        },

        // Обработка смены состояний списков
        handleSelectStates: function ($select, selectActionObjects, actOnPageLoad) {
            var handler = function () {
                var $selectedOption = $select.find('option:selected');
                var selectedId = -1;
                for (var i = 0; i < selectActionObjects.length; i++) {
                    if ($selectedOption.val() === selectActionObjects[i].value || $selectedOption.text() === selectActionObjects[i].text) {
                        selectActionObjects[i].actOnSelect();
                        selectedId = $select.val();
                    }
                    if ($selectedOption.val() !== selectActionObjects[i].value && $selectedOption.text() !== selectActionObjects[i].text && $selectedOption.val() !== selectedId) {
                        selectActionObjects[i].actOnDeselect();
                    }
                }

            };

            $select.bind("change", handler);

            if (actOnPageLoad === true) {
                handler();
            }
        },

        // Добавить datepicker к текстовым контролам
        // $inputs - jQuery-объект с выборкой контролов
        // range - строка формата "диапазон_лет[;минимальная_дата[;максимальная_дата]]", пример: "-80:-14;-80Y;-14Y"
        // makeReadonly - заблокировать ввод в контрол с помощью клавиатуры
        attachDatepickerToInputs: function ($inputs, range, makeReadonly) {
            var rangeArray = range.split(";");
            $inputs.datepicker({
                dateFormat: "dd.mm.yy",
                changeYear: true,
                yearRange: rangeArray[0] !== undefined ? rangeArray[0] : null,
                minDate: rangeArray[1] !== undefined ? rangeArray[1] : null,
                maxDate: rangeArray[2] !== undefined ? rangeArray[2] : null
            });
            if (makeReadonly) {
                $inputs.attr("readOnly", "true");
            }
        },

        // Преобразование Date в ShortDateString
        convertDateToShortDateString: function (date) {
            if (!date) {
                return "__.__.____"
            }
            return ('0' + date.getDate()).slice(-2) + "." + ('0' + (date.getMonth() + 1)).slice(-2) + "." + date.getFullYear();
        },

        // Преобразование формата даты, передаваемой ASP.NET MVC в JSON в ShortDateString
        convertMSJsonDateToShortDateString: function (mSJsonDate) {
            if (!mSJsonDate) {
                return "__.__.____"
            }
            var date = new Date(parseInt(mSJsonDate.substr(6)));
            return ('0' + date.getDate()).slice(-2) + "." + ('0' + (date.getMonth() + 1)).slice(-2) + "." + date.getFullYear();
        },

        // Отображение результата операции с данными текстового поля временным изменением цвета
        showInputActionResult: function (inputs, isOk) {
            var color = isOk ? "lightgreen" : "pink";
            for (var i = 0; i < inputs.length; i++) {
                inputs[i].css("background-color", color);
            }
            setTimeout(function () {
                for (var i = 0; i < inputs.length; i++) {
                    inputs[i].css("background-color", "white");
                }
            }, 700);
        }

    };
} ());