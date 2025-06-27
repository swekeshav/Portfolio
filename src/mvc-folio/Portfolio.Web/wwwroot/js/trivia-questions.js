$(document).ready(function () {
	var triviaTable = $("#triviaQuestionsTable").DataTable(
		{
			"pageLength": 10,
			"dom": "ltip",
			"lengthMenu": [[10, 20, 30], [10, 20, 30]],
			"ajax": {
				"url": "/api/trivia/questions",
				"datatype": "json",
				"data": function (d) {
					d.Category = $("#trivia-category").val();
					d.Difficulty = $("#trivia-difficulty").val();
				},
				"dataSrc": function (json) {
					console.log(json);
					if (json.hasFailed) {
						console.log('Something went wrong. Please try again later.');
					}
					return json?.data;
				}
			},
			"columns": [
				{ "data": "category", "name": "Category" },
				{ "data": "difficulty", "name": "Difficulty" },
				{ "data": "question", "name": "Question" },
			],
			"language": {
				"emptyTable": "Enough data may not be available.",
				"zeroRecords": "Nothing found - sorry :(",
			},
		});

	var keyupTimeout;
	triviaTable.columns().every(function () {
		$("select", this.footer()).on('keyup change', function (event) {
			if (keyupTimeout) {
				clearTimeout(keyupTimeout);
			}
			keyupTimeout = setTimeout(function () {
				triviaTable.ajax.reload();
			}, 1000);
		})
	});

	$("#triviaQuestionsTable tfoot tr").appendTo("#triviaQuestionsTable thead")
});