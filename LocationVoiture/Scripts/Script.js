

$(document).ready(function () {
    hideOnLoad();
    $('.sideMenuToggler').on('click', function () {
        $('.wrapper').toggleClass('active');

        console.log("clicked");

    });

    var adjustSidebar = function () {
        $('.sidebar').slimScroll({
            height: document.documentElement.clientHeight - $('.navbar').outerHeight()
        });
    };

    adjustSidebar();
    $(window).resize(function () {
        adjustSidebar();
    });


    /*  $("#UserType").change(function () {
          console("clicked");
          var val = $('#type option:selected').val();
          console.log(val);

      });*/


    var frensh = {
        "language": {
            "sProcessing": "Traitement en cours ...",
            "sLengthMenu": "Afficher _MENU_ lignes",
            "sZeroRecords": "Aucun résultat trouvé",
            "sEmptyTable": "Aucune donnée disponible",
            "sInfo": "Lignes _START_ à _END_ sur _TOTAL_",
            "sInfoEmpty": "Aucune ligne affichée",
            "sInfoFiltered": "(Filtrer un maximum de_MAX_)",
            "sInfoPostFix": "",
            "sSearch": "Chercher:",
            "sUrl": "",
            "sInfoThousands": ",",
            "sLoadingRecords": "Chargement...",
            "oPaginate": {
                "sFirst": "Premier", "sLast": "Dernier", "sNext": "Suivant", "sPrevious": "Précédent"
            },
            "oAria": {
                "sSortAscending": ": Trier par ordre croissant", "sSortDescending": ": Trier par ordre décroissant"
            }
        }
    }
    if (Cookies.get('culture') === "fr") {
        window.$('.table').DataTable(frensh);

        $("#selected-language").html("<img src='https://www.countryflags.io/fr/flat/32.png' class='icon-country' /> Français");

    } else {
        window.$('.table').DataTable();
        $("#selected-language").html("<img src='https://www.countryflags.io/us/flat/32.png' class='icon-country' /> English");


    }

    $(function () {
        console.log(Cookies.get('culture'));

    });

    $(function () {
        $("a.delete-link-offre").click(function () {

            var token = $("[name='__RequestVerificationToken']").val();

            console.log(token);
            var checkstr;
            if (Cookies.get('culture') === "fr") {
                checkstr = confirm('Voullez vous vraiment supprimer cette Offre?');

            } else {
                checkstr = confirm('Are you sure you want to delete this?');
            }
            if (checkstr == true) {
                $.ajax({
                    url: '/Offres/Delete/' + $(".delete-link-offre").attr('data-delete-id'),
                    type: "POST",
                    data: {
                        __RequestVerificationToken: token,
                    },
                    success: function () {
                        window.location.replace("https://localhost:44333/Offres/index");
                    }
                });
            }
        }
        );
    });

    $(function () {
        $("a.delete-link-voiture").click(function () {

            var token = $("[name='__RequestVerificationToken']").val();

            console.log(token);
            var checkstr;
            if (Cookies.get('culture') === "fr") {
                checkstr = confirm('Voullez vous vraiment supprimer cette Voiture?');

            } else {
                checkstr = confirm('are you sure you want to delete this?');
            }
            if (checkstr == true) {
                $.ajax({
                    url: '/Voitures/Delete/' + $(".delete-link-voiture").attr('data-delete-id'),
                    type: "POST",
                    data: {
                        __RequestVerificationToken: token,
                    },
                    success: function () {
                        window.reload();
                    }
                });
            }
        }
        );
    });

    $(function () {
        $("a.block-link-user").click(function () {
            var token = $("[name='__RequestVerificationToken']").val();
            var user_id = $(this).attr('data-block-id');
            var color= $(this).attr('data-color');
            console.log(token);
            var checkstr;
            if (Cookies.get('culture') === "fr") {
                if (color == "red")
                    checkstr = confirm('Voullez vous vraiment bloquer cet utilisateur?');
                else
                    checkstr = confirm('Voullez vous vraiment débloquer cet utilisateur?');

            } else
            {
                if (color == "red")
                    checkstr = confirm('Are you sure you want to block this user?');
                else
                    checkstr = confirm('Are you sure you want to unblock this user?');
                
            }
            if (checkstr == true) {
                $.ajax({
                    url: '/ApplicationUsers/Delete/' + user_id,
                    type: "POST",
                    data: {
                        __RequestVerificationToken: token,
                    },
                    success: function () {
                        window.location.replace("https://localhost:44333/ApplicationUsers/Index");
                    }
                });
            }
        }
        );
    });

    $(function () {
        $("a.delete-link-reserve").click(function () {

            var token = $("[name='__RequestVerificationToken']").val();

            console.log(token);
            var checkstr;
            if (Cookies.get('culture') === "fr") {
                checkstr = confirm('Voullez vous vraiment supprimer cette réservation?');

            } else {
                checkstr = confirm('are you sure you want to delete this reservation?');
            }
            if (checkstr == true) {
                $.ajax({
                    url: '/Reservations/Delete/' + $(".delete-link-reserve").attr('data-delete-id'),
                    type: "POST",
                    data: {
                        __RequestVerificationToken: token,
                    },
                    success: function () {
                        window.location.reload();
                    }
                });
            }
        }
        );
    });
    $(function () {
        $("a.delete-link-favorite").click(function () {

            var token = $("[name='__RequestVerificationToken']").val();

            console.log(token);
            var checkstr;
            if (Cookies.get('culture') === "fr") {
                checkstr = confirm('Voullez vous vraiment supprimer cet utilisateur de la liste de favoris?');

            } else {
                checkstr = confirm('are you sure you want to delete this user to your favorite list?');
            }
            if (checkstr == true) {
                $.ajax({
                    url: '/FavoriteLists/Delete/' + $(".delete-link-favorite").attr('data-delete-id'),
                    type: "POST",
                    data: {
                        __RequestVerificationToken: token,
                    },
                    success: function () {
                        window.location.reload();
                    }
                });
            }
        }
        );
    });
    $(function () {
        $("a.delete-link-black").click(function () {

            var token = $("[name='__RequestVerificationToken']").val();

            console.log(token);
            var checkstr;
            if (Cookies.get('culture') === "fr") {
                checkstr = confirm('Voullez vous vraiment supprimer cet utilisateur de la liste noire?');

            } else {
                checkstr = confirm('are you sure you want to delete this user to your black list?');
            }
            if (checkstr == true) {
                $.ajax({
                    url: '/BlackLists/Delete/' + $(".delete-link-black").attr('data-delete-id'),
                    type: "POST",
                    data: {
                        __RequestVerificationToken: token,
                    },
                    success: function () {
                        window.location.reload();
                    }
                });
            }
        }
        );
    });

    $(function () {
        $('#table-user tbody').on('click', 'a.add-link-favorite', function () {

            var token = $("[name='__RequestVerificationToken']").val();
            console.log($(this).attr('data-add-id'));
            var user_id = $(this).attr('data-add-id');
            var checkstr;
            if (Cookies.get('culture') === "fr") {
                checkstr = confirm('Voullez vous ajouter cet utilisateur à la liste de favoris?');

            } else {
                checkstr = confirm('Are you sure you want to add this user to your favorite list?');
            }
            if (checkstr == true) {
                $.ajax({
                    url: '/FavoriteLists/Create/' + user_id,
                    type: "POST",
                    data: {
                        __RequestVerificationToken: token,
                    },
                    success: function () {
                        window.location.reload();
                    }
                });
            }
        }
        );
    });

    $(function () {
        $('#table-user tbody').on('click', 'a.add-link-black', function () {

            var token = $("[name='__RequestVerificationToken']").val();
            console.log($(this).attr('data-add-id'));
            var user_id = $(this).attr('data-add-id');
            var checkstr;
            if (Cookies.get('culture') === "fr") {
                checkstr = confirm('Voullez vous ajouter cet utilisateur à la liste de noire?');

            } else {
                checkstr = confirm('Are you sure you want to add this user to your black list?');
            }
            if (checkstr == true) {
                $.ajax({
                    url: '/BlackLists/Create/' + user_id,
                    type: "POST",
                    data: {
                        __RequestVerificationToken: token,
                    },
                    success: function () {
                        window.location.reload();
                    }
                });
            }
        }
        );
    });

    $(function () {

        $("#listPage").JPaging({
            pageSize: 5
        });

    });

    function hideOnLoad() {
        $('#typeOwner').hide();
    }
    $('#usertype').change(function () {
        var value = $(this).val();
        if (Cookies.get('culture') === "fr")
        {
            if (value == 'Locataire') {
                $('#typeOwner').hide();
            } else if (value == 'Propriétaire') {
                $('#typeOwner').show();
            } else {
                $('#typeOwner').hide();
            }
        }
        else
        {
            if (value == 'Tenant') {
                $('#typeOwner').hide();
            } else if (value == 'Owner') {
                $('#typeOwner').show();
            } else {
                $('#typeOwner').hide();
            }
        }    
    });
});
