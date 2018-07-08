/// <reference path="../jquery.validate.js" />

$(document).ready(function () {
    var vm = {
        movieIds: []
    };
    var customers = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('Name'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        remote: {
            url: '/api/customers?query=%QUERY',
            wildcard: '%QUERY'
        }
    });

    $('#rentalcustomer').typeahead({
        minLength: 3,
        hint: true,
        highlight: true
    }, {
            name: 'Name',
            display: 'Name',
            source: customers,
            limit: 10
        }).on("typeahead:select", function (e, customer) {
            vm.customerId = customer.Id;
        });

    var movies = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('Name'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        remote: {
            url: '/api/movies?query=%QUERY',
            wildcard: '%QUERY'
        }
    });

    $('#rentalmovie').typeahead({
        minLength: 3,
        hint: true,
        highlight: true
    }, {
            name: 'movies',
            displayKey: 'Name',
            source: movies,
            limit: 10
        }).on("typeahead:select", function (e, movie) {
            $('#movies').append(
                `<li class="list-group-item">${movie.Name}</li>`
            );
            $('#rentalmovie').typeahead("val", "");
            vm.movieIds.push(movie.Id);
        });

    $.validator.addMethod('validCustomer', function () {
        return (vm.customerId && vm.customerId != 0);
    }, "Please select a valid customer");

    $.validator.addMethod('atLeastOneMovie', function () {
        return vm.movieIds.length > 0;
    }, "Please select at least one movie");

    var validator=$('#newRental').validate({
        submitHandler: function () {
            $.ajax({
                url: '/api/newrentals',
                method: 'POST',
                data: vm
            }).done(function () {
                toastr.success('Rentals successfully recorded');
                $('#rentalcustomer').typeahead("val", "");
                $('#rentalmovie').typeahead("val", "");
                $('#movies').empty();
                vm = { movieIds: [] };
                validator.resetForm();
            }).fail(function () {
                console.log('not done');
                toastr.error('Rentals could not recorded');
            });
            return false;
        }
    });
});