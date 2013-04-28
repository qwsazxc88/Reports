    function setSortFields(){
        if(($("#SortBy").val() != 0) && ($("#SortDescending").val() != ''))
        {
            if($("#SortDescending").val() == 'True')
                $("#sort"+$("#SortBy").val()).addClass("sort-desc");
            else        
                $("#sort"+$("#SortBy").val()).addClass("sort-asc");
        }
    }
    function SortChanged(sortedBy)
    {
        if($("#SortBy").val() != sortedBy)
            $("#SortDescending").val(''); 
        $("#SortBy").val(sortedBy);
        if($("#SortDescending").val() == '')
            $("#SortDescending").val('True');
        else if($("#SortDescending").val() == 'True')
            $("#SortDescending").val('False');
        else
            $("#SortDescending").val('True');
         submitForm();
         return false;
    }
    function submitForm() {
        if (document.forms[0].onsubmit && !document.forms[0].onsubmit())
            return;
        document.forms[0].submit();
    }   
