﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {

    //************************************************* INICIALIZAÇÃO DE COMPONENTES *************************************************
    $('.sidenav').sidenav();
    $('.dropdown-trigger').dropdown();
    $('.tooltipped').tooltip();
    $('.fixed-action-btn').floatingActionButton();
    $('select').formSelect();
    
    //************************************************* VARIAVEIS DE AMBIENTE  *******************************************************

    var errorMsg = '<div class="col row z-depth-3 teal white-text center-align" style="height: 50px; border: 1px solid; border-radius: 10px; padding:12px 0px 0px 0px;">';
    errorMsg += '<div class="col s1 left-align"><i class="material-icons white-text">loop</i></div>';
    errorMsg += '<div class="col s11 center">Ocorreu um erro na requisição do serviço, atualize a pagina e tente novamente!</div></div>';

    //************************************************* MASCARAS / VALIDAÇÕES/ FUNÇÕES  **********************************************
    $("#Cnpj").mask("99.999.999/9999-99");
    $("#Cpf").mask("999.999.999-99");
    $("#Nu_Oab").mask("999999/##", { translation: { '#': { pattern: /[^0-9$]/, optional: true } } });

    $("#clearFrm").click(() => {
        //$("#Cnpj").val(null);
        //$("#empresa").val('');
        //$('select').find('option[value="0"]').prop('selected', true);
        //$('select').formSelect();

        $('form').trigger("reset");
        $("#Status").prop("checked", true);
    });

    $("#btnClear").click(() => {
        $('form').trigger("reset");
    });


    //************************************************* CONSULTAR GENERIA **********************************************************
    $("#btnSearch").bind('click', function (event, param) {
        let body = {};
        var tipo = location.pathname.replace(/[^\w\s]/gi, '')
        body = getObejt(tipo);

        if (param == "&gt;&gt;") // >>
            param = parseInt($('#pageCount').val());

        if (param == "&lt;&lt;") // <<
            param = 1;

        $('#DivRenderData').css("display", "");;
        $('#DivRenderData').empty().html('<div class="progress"><div class="indeterminate"></div></div>');

        var action = location.href + '/Search/' + (isNaN(param) ? 1 : param);

        jQuery.ajax({
            type: 'Get',
            url: action,
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            data: body
        })
            .done(function (data) {
                $("#DivRenderData").empty().html(data);
                
            })
            .fail(function (data) {
                $("#DivRenderData").empty().html(errorMsg);
            });
    });

    //******************************************************************************************************************************

    //************************************************* AUTO-COMPLETE **************************************************************
    $('.autocomplete').on('input', function () {

        let filter = $('.autocomplete').val();

        if (filter.length < 3)
            return;
        var tipo = '/' + location.pathname.split('/')[1] + '/GetAutoComplete';
        jQuery.ajax({
            type: 'Get',
            url: tipo,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: { _campo: filter }
        })
            .done(function (dataComplete) {

                var dataCountry = {};
                dataComplete.forEach(function (v) {
                    dataCountry[v.text] = null ;
                });

                $('input.autocomplete').autocomplete({
                    data: dataCountry
                });

                $('input.autocomplete').autocomplete('open')
                document.getElementsByName('action')[0].disabled = dataComplete.length > 0;

            })
            .fail(function () {
                M.toast({ html: '<pre><i class="material-icons white-text">report</i> ERRO: AUTO-COMPLETE!<p>Ocorreu uma falha na busca de valores do auto-complete.</p></pre>', classes: 'gray darken-3 rounded' });
            });
    });

    //******************************************************************************************************************************


});


var getObejt = function (patname) {

    let obj = {};
    switch (patname) {
        case 'Empresa':
            obj = {
                no_empresa: $("#Empresa").val().toUpperCase(),
                nu_cnpj: $("#Cnpj").val(),
                statusfilter: $("#StatusFilter").val() 
            }
            return { empresa: JSON.stringify(obj) };

        case 'Documento':
             obj = {
                ds_documento: $("#Documento").val(),
                 cd_tipo_documento: $('#Tipo').val(),
                statusfilter: $("#StatusFilter").val() 
            }
            return { documento: JSON.stringify(obj) };

        case 'Comarca':
            obj = {
                sg_uf: $("#Sigla").val(),
                no_comarca: $("#cidade").val(),
                statusfilter: $("#StatusFilter").val()
            }
            return { comarca: JSON.stringify(obj) };

        case 'Contrato':
            obj = {
                ds_contrato: $("#Contrato").val(),
                statusfilter: $("#StatusFilter").val()
            }
            return { contrato: JSON.stringify(obj) };

        case 'Familia':
            obj = {
                Ds_Grupo_Familia: $("#Familia").val(),
                statusfilter: $("#StatusFilter").val()
            }
            return { familia: JSON.stringify(obj) };

        case 'Link':
            obj = {
                no_sistema: $("#Sistema").val(),
                statusfilter: $("#StatusFilter").val()
            }
            return { link: JSON.stringify(obj) };

        case 'Pedido':
            obj = {
                ds_tipo_pedido: $("#dspedido").val(),
                statusfilter: $("#StatusFilter").val()
            }
            return { pedido: JSON.stringify(obj) };

        case 'MotivoAvaliacao':
            obj = {
                ds_motivo: $("#ds_motivo").val(),
                tipo: $("#tipo").val(),
                statusfilter: $("#StatusFilter").val()
            }
            return { motivo: JSON.stringify(obj) };

        case 'Usuario':
            obj = {
                nome: $("#nome").val(),
                login: $("#login").val(),
                statusfilter: $("#StatusFilter").val(),
            }
            return { usuario: JSON.stringify(obj) };

        case 'Questionamento':
            obj = {
                ds_questao: $("#ds_questao").val(),
                cd_tipo_questionamento: $("#cd_tipo_questionamento").val(),
                cd_tipo_contrato: $("#cd_tipo_contrato").val(),
                statusfilter: $("#StatusFilter").val()
            }
            return { questionamento: JSON.stringify(obj) };

        case 'Advogado':
            obj = {
                nu_oab: $("#Nu_Oab").val(),
                no_advogado: $("#no_advogado").val(),
                tipo_advogado: $("#tipo_advogado").val(),
                statusfilter: $("#StatusFilter").val()
            }
            return { advogado: JSON.stringify(obj) };

        case 'OrigemEnvolvida':
            obj = {
                cd_empresa_envolvida: $("#Cd_Empresa_Envolvida").val(),
                statusfilter: $("#StatusFilter").val()
            }
            return { origemenvolvida: JSON.stringify(obj) };

        case 'Calendario':
            obj = {
                dt_nacional: $("#dt_nacional").val(),
                sg_uf: $("#sg_uf").val(),
                ds_titulo_data: $("#ds_titulo_data").val(),
                statusfilter: $("#StatusFilter").val()
            }
            return { calendario: JSON.stringify(obj) };

        default:
            return { id: 100, error: 'caminho desconhecido' };
    }
 
}

function validarCPF(inputCPF) {
    var soma = 0;
    var resto;
    var num = '';
    var count = 0;

    do {

        num = count.toString();

        for (var i = 0; i < 10; i++) {
            num += count.toString();
        }

        if (num == inputCPF)
            return false;
        
    } while (count++ < 10);

    for (i = 1; i <= 9; i++) soma = soma + parseInt(inputCPF.substring(i - 1, i)) * (11 - i);
    resto = (soma * 10) % 11;

    if ((resto == 10) || (resto == 11)) resto = 0;
    if (resto != parseInt(inputCPF.substring(9, 10))) return false;

    soma = 0;
    for (i = 1; i <= 10; i++) soma = soma + parseInt(inputCPF.substring(i - 1, i)) * (12 - i);
    resto = (soma * 10) % 11;

    if ((resto == 10) || (resto == 11)) resto = 0;
    if (resto != parseInt(inputCPF.substring(10, 11))) return false;
    return true;
}