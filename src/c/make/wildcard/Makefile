# --------------------------------

SOURCE_FILES = $(wildcard *.c)

# Print out file information about every .c file
print1: $(wildcard *.c)
	ls -la  $?

# Print out file information about every .c file
print2: $(SOURCE_FILES)
	ls -la  $?

# --------------------------------

hey: one two
	# Outputs "hey", since this is the target name
	echo $@

	# Outputs all prerequisites newer than the target
	echo $?

	# Outputs all prerequisites
	echo $^

	touch hey

one:
	touch one

two:
	touch two

clean:
	rm -f hey one two