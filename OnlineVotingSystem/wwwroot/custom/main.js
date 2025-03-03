$(document).ready(function () {
    // --------------------------- Toastr ---------------------------
    const successMessage = $('#successMessage').text().trim();
    if (successMessage !== '') toastr.success(successMessage);

    const errorMessage = $('#errorMessage').text().trim();
    if (errorMessage !== '') toastr.error(errorMessage);

    const infoMessage = $('#infoMessage').text().trim();
    if (infoMessage !== '') toastr.info(infoMessage);

    const warningMessage = $('#warningMessage').text().trim();
    if (warningMessage !== '') toastr.warning(warningMessage);
    // --------------------------- Toastr ---------------------------

    // --------------------------- Sort Table ---------------------------
    const currentTitlePage = $("title").text();

    const projectTitle = "Online Voting System";
    const dash = "-";
    const space = " ";

    if (
        (currentTitlePage === "Election Management" + space + dash + space + projectTitle)
        ||
        (currentTitlePage === "Candidate Management" + space + dash + space + projectTitle)
        ||
        (currentTitlePage === "View Election Results" + space + dash + space + projectTitle)
    ) {
        enableSorting("sortTable", "sortTableHeader1", 0);
        enableSorting("sortTable", "sortTableHeader2", 1);
        enableSorting("sortTable", "sortTableHeader3", 2);
        enableSorting("sortTable", "sortTableHeader4", 3);
        enableSorting("sortTable", "sortTableHeader5", 4);
    }
    else if (
        (currentTitlePage === "Vote in an Ongoing Election" + space + dash + space + projectTitle)
        ||
        (currentTitlePage === "Your Voting History" + space + dash + space + projectTitle)
    ) {
        enableSorting("sortTable", "sortTableHeader1", 0);
        enableSorting("sortTable", "sortTableHeader2", 1);
        enableSorting("sortTable", "sortTableHeader3", 2);
        enableSorting("sortTable", "sortTableHeader4", 3);
    }
    // --------------------------- Sort Table ---------------------------

});