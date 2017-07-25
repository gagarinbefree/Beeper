var app = (function () {
    function app() {
        this.init();
    }

    app.prototype.init = function () {
        $('#txtIsvalid').multiselect({ nonSelectedText: 'Валидный', allSelectedText: 'Валидный' });
        var city = new multiselector($('#txtCity'), $('#filterCity'), 'Город', true, '/Home/GetCities');
        var category = new multiselector($('#txtCategory'), $('#filterCategory'), 'Категория', true, '/Home/GetCategories');        

        var loader = new xlsuploader();

        var grid = $('#grid').grid({
            primaryKey: 'id',
            dataSource: '/Home/GetPersons',
            columns: [
                { field: 'id', title: 'ID', sortable: true },
                { field: 'lastname', title: 'Фамилия', sortable: true },
                { field: 'firstname', title: 'Имя', sortable: true },
                { field: 'middlename', title: 'Отчество', sortable: true },
                { field: 'phone', title: 'Телефон', sortable: true },
                { field: 'birthday', title: 'Дата рождения', sortable: true },
                { field: 'city', title: 'Город', sortable: true },
                { field: 'sex', title: 'Пол', sortable: true },
                { field: 'category', title: 'Категория', sortable: true },
                { field: 'isvalid', title: 'Валидный', sortable: true }
            ],
            pager: { limit: 10 },
            uiLibrary: 'bootstrap'
        });

        $('#btnSearch').on('click', function () {
            grid.reload({
                lastname: $('#txtLastName').val(),
                phone: $('#txtPhone').val(),
                city: $('#txtCity').val() != null ? $('#txtCity').val().toString() : '',
                category: $('#txtCategory').val() != null ? $('#txtCategory').val().toString() : '',
                isvalid: $('#txtIsvalid').val() != null ? $('#txtIsvalid').val().toString() : '',
            });
        });

        $('#btnClear').on('click', function () {
            $('#txtLastName').val('');
            $('#txtPhone').val('');

            $('#txtCity').multiselect("deselectAll", false).multiselect("refresh");
            $('#txtCategory').multiselect("deselectAll", false).multiselect("refresh");
            $('#txtIsvalid').multiselect("deselectAll", false).multiselect("refresh");

            grid.reload({
                lastname: '',
                phone: '',
                city: '',
                category: '',
                isvalid: '',
            });
        });        
    }

    return app;
})();