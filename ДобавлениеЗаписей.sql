insert into cd_specialty (c_specialty_title, c_specialty_code)
Select 'Прикладная информатика', '09.03.03';

insert into cd_specialty (c_specialty_title, c_specialty_code)
Select 'Программная инженерия', '09.03.04';

insert into cd_group (c_group_name, c_group_course, f_specialty_id, c_group_isDeleted)
Select 'KT-41-20', 1, 1, 0;

insert into cd_group (c_group_name, c_group_course, f_specialty_id, c_group_isDeleted)
Select 'KT-42-20', 1, 2, 0;

insert into cd_student(c_student_firstname, c_student_lastname, f_group_id, c_student_isDeleted)
Select 'Ivan','Ivanov', 1, 0;

insert into cd_student(c_student_firstname, c_student_lastname, f_group_id, c_student_isDeleted)
Select 'Viktor','Nikolaev', 2, 0;