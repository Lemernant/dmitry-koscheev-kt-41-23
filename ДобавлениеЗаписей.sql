insert into cd_specialty (c_specialty_title, c_specialty_code)
Select N'сп1', N'09.03.03';

insert into cd_specialty (c_specialty_title, c_specialty_code)
Select N'сп2', N'09.03.04';
GO

insert into cd_group (c_group_name, c_group_course, f_specialty_id, c_group_isDeleted)
Select N'гр1', 1, 1, 0;

insert into cd_group (c_group_name, c_group_course, f_specialty_id, c_group_isDeleted)
Select N'гр2', 1, 1, 0;

insert into cd_group (c_group_name, c_group_course, f_specialty_id, c_group_isDeleted)
Select N'гр3', 1, 2, 0;
GO

insert into cd_student(c_student_firstname, c_student_lastname, f_group_id, c_student_isDeleted)
Select N'Иван',N'иванов', 1, 0;

insert into cd_student(c_student_firstname, c_student_lastname, f_group_id, c_student_isDeleted)
Select N'',N'петров', 2, 0;

insert into cd_student(c_student_firstname, c_student_lastname, f_group_id, c_student_isDeleted)
Select N'петр',N'иванов', 1, 0;

insert into cd_student(c_student_firstname, c_student_lastname, f_group_id, c_student_isDeleted)
Select N'',N'сидоров', 3, 0;
GO

insert into cd_discipline(c_discipline_name, c_discipline_isDeleted)
Select N'Математика', 0;

insert into cd_discipline(c_discipline_name, c_discipline_isDeleted)
Select N'История', 0;
GO

insert into cd_grade(c_grade_value, f_student_id, f_discipline_id)
Select 4, 1, 1;

insert into cd_grade(c_grade_value, f_student_id, f_discipline_id)
Select 3, 1, 2;
GO

insert into cd_grade(c_grade_value, f_student_id, f_discipline_id)
Select 5, 2, 1;

insert into cd_grade(c_grade_value, f_student_id, f_discipline_id)
Select 4, 2, 2;